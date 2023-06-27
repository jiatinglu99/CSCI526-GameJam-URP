using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public float speed = 80.0f;
    public float turnSpeed = 20.0f;
    public float axisLockY = 27.5f;
    public Vector3 lastFlashlightLocation;
    private Rigidbody rb;

    private FlashlightControl flashlightControl;
    public GameObject victoryScreen; // Reference to the victory screen UI object
    private bool completedLevel = false; // used for analytics to ignore multiple collisions with end goal
    private string curLevel = "Level-1";
    

    public Canvas popupCanvas;
    
    [SerializeField]
    public PopupController popupController;
    public CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        flashlightControl = GetComponent<FlashlightControl>();
        
        Debug.Log("PlayerControl.Awake()");
        popupCanvas.enabled = true;
        popupController.ShowPopup("You need to reach to the Green GOAL!");

        //wait for 3 seconds
        StartCoroutine(DisablePopupAfterDelay());

        // popupCanvas.enabled = false;
        lastFlashlightLocation = transform.position;
        cameraController.ZoomOutCamera();
    }

    IEnumerator DisablePopupAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        popupCanvas.enabled = false;
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical   = Input.GetAxis("Vertical");

        Vector3 movement;
        if (moveHorizontal != 0)
        {
            movement = new Vector3(moveHorizontal, 0, 0);
        }
        else if (moveVertical != 0)
        {
            movement = new Vector3(0, 0, moveVertical);
        }
        else
        {
            movement = Vector3.zero;
        }
        movement = movement.normalized * speed * Time.fixedDeltaTime;

        // rotate the player to face the moving direction
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
            // rb.MovePosition() does not work well with wall collision
            // rb.MovePosition(transform.position - movement);
        }
        // modify velocity directly works very well with wall collision
        rb.velocity = -movement*speed;

        // prevent effect from collision
        // rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        // lock y axis
        rb.MovePosition(new Vector3(rb.position.x, axisLockY, rb.position.z));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            // Display the victory screen
            // victoryScreen.SetActive(true);
            Destroy(collision.gameObject);
            popupController.ShowPopup("You Win! Press Enter to proceed to the next level...");
            popupCanvas.enabled = true;

            if (!completedLevel)
            {
                // update the analytics metric "highest completed level"
                Analytics.playerData.highestCompletedLevel += 1;
                Analytics.updateDatabase();
                completedLevel = true;
            }

            // Disable the player movement
            // Assuming you have a script controlling the player's movement,
            // you can disable it by finding and disabling the script component
            PlayerControl playerMovement = GetComponent<PlayerControl>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }
            string currentSceneName = SceneManager.GetActiveScene().name;
            if(string.Equals("Level-1",currentSceneName))
                curLevel = "Level-2";
            else if(string.Equals("Level-2",currentSceneName))
                curLevel = "Level-3";
            else
                curLevel = "Level-1";

            StartCoroutine(LoadLevel());
        }

        if (collision.gameObject.CompareTag("Monster"))
        {
            // Display the victory screen
            // victoryScreen.SetActive(true);
            popupController.ShowPopup("You Lose! Press Enter to retry the level.");
            popupCanvas.enabled = true;
            // Disable the player movement
            // Assuming you have a script controlling the player's movement,
            // you can disable it by finding and disabling the script component
            PlayerControl playerMovement = GetComponent<PlayerControl>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }

            StartCoroutine(LoadLevel());
        }

        if (collision.gameObject.CompareTag("Coin"))
        {

            // Handle collision with the "Target" object

            // Handle collision logic here
            Debug.Log("Collision occured");
            // Destroy the collided object
            Destroy(collision.gameObject);
            // Refill flashlight battery
            flashlightControl.RefillFlashlightBattery();
        }
    }

    IEnumerator LoadLevel()
    {
        // Wait until return key is pressed
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }

        completedLevel = false; // sets for upcoming/reloaded level

        SceneManager.LoadScene(curLevel);
    }

}