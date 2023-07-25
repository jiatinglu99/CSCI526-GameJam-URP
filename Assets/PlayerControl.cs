using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using System;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    public float speed = 80.0f;
    public float turnSpeed = 20.0f;
    public float axisLockY = 27.5f;
    
    public bool isSanity = false;
    public float health = 100.0f;
    public float healthDrainer = 100;
    private bool alreadyWon = false;

    public Vector3 lastFlashlightLocation;
    private Rigidbody rb;

    private FlashlightControl flashlightControl;
    public GameObject victoryScreen; // Reference to the victory screen UI object
    private bool completedLevel = false; // used for analytics to ignore multiple collisions with end goal
    private int curLevel;
    private Stopwatch stopWatch = new Stopwatch();

    public Light pointLight;


    private Canvas popupCanvas;
    
    [SerializeField]
    private PopupController popupController;
    public CameraController cameraController;
    public GameObject UIDocument_pause;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        flashlightControl = GetComponent<FlashlightControl>();

        UnityEngine.Debug.Log("PlayerControl.Awake()");
        popupCanvas = GameObject.Find("CanvasPopup").GetComponent<Canvas>();
        popupController = GameObject.Find("CanvasPopup").GetComponent<PopupController>();
        popupCanvas.enabled = true;
        UIDocument_pause.SetActive(false);

        popupController.ShowPopup("You need to reach to the Green GOAL!");

        // Disable the player movement at start
        PlayerControl playerMovement = GetComponent<PlayerControl>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        //wait for 3 seconds
        StartCoroutine(DisablePopupAfterDelay());

        // popupCanvas.enabled = false;
        lastFlashlightLocation = transform.position;
        cameraController.ZoomOutCamera();
        curLevel = SceneManager.GetActiveScene().buildIndex;

        // (re)start stopwatch for level
        stopWatch.Start();
        
        alreadyWon = false;

        pointLight = GameObject.Find("/Player/PointLight").GetComponent<Light>();
        pointLight.enabled = false;
    }

    IEnumerator DisablePopupAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        popupCanvas.enabled = false;
        UIDocument_pause.SetActive(true);
        // UIDocument uidoc = UIDocument_pause.GetComponent<UIDocument>();
        // VisualElement root = uidoc.rootVisualElement;
        // Label myLabel = root.Q<Label>("Battery");
        // myLabel.text = "New Text";

        // resume player movement
        PlayerControl playerMovement = GetComponent<PlayerControl>();
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical   = Input.GetAxisRaw("Vertical");

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

    void FixedUpdate()
    {
        
        if(UIDocument_pause.activeSelf)
        {
            UIDocument uidoc = UIDocument_pause.GetComponent<UIDocument>();
            VisualElement root = uidoc.rootVisualElement;
            Label myLabel = root.Q<Label>("Battery");
            myLabel.text = "Flashlight "+flashlightControl.GetFlashlightBatteryLevel()+"%";

            Label myLabel2 = root.Q<Label>("Health");
            myLabel2.text = "Sanity "+(int)health+"%";   
            if(flashlightControl.GetFlashlightBatteryLevel()<=5)
            {
                // isSanity = true;
                pointLight.enabled = true;
                DrainHealth();
            }
            else
            {
                health += Time.deltaTime * healthDrainer / 20;
                // max health at 100
                health = Math.Min(health, 100);
            }
                
        }

    }

    void DrainHealth()
    {
        UnityEngine.Debug.Log("Trigger Sanity");
        if(health>0)
            health -= Time.deltaTime * healthDrainer/4;
        else
        {
            popupController.ShowPopup("You Died! Press Enter to retry the level.");
            popupCanvas.enabled = true;
            UIDocument_pause.SetActive(false);
            StartCoroutine(LoadLevel());

            PlayerControl playerMovement = GetComponent<PlayerControl>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }

        }

        UnityEngine.Debug.Log("Trigger Sanity: "+ health);
    }

    void OnCollisionEnter(Collision collision)
    {

        string currentSceneName = SceneManager.GetActiveScene().name;
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (collision.gameObject.CompareTag("Finish"))
        {
            // Display the victory screen
            // victoryScreen.SetActive(true);
            // Destroy(collision.gameObject);
            collision.gameObject.GetComponent<ShatterUponCollision>().Shatter();
            alreadyWon = true;
            popupController.ShowPopup("You Escaped!\nPress Enter to proceed to the next level...");
            popupCanvas.enabled = true;
            UIDocument_pause.SetActive(false);

            // ensures that multiple collisions don't trigger the counter to increment more than once
            if (!completedLevel)
            {
                // update the analytics metric "highest completed level"
                Analytics.playerData.highestCompletedLevel += 1;
                completedLevel = true;

                // stop the stopwatch for level
                stopWatch.Stop();
                long time = stopWatch.ElapsedMilliseconds;
                UnityEngine.Debug.Log($"Stopwatch {time}");
                Analytics.playerData.timeSpent[curSceneIndex] = (time/1000); // converting milliseconds to seconds. Now that's analytics!

                Analytics.updateDatabase();
            }

            // Disable the player movement
            // Assuming you have a script controlling the player's movement,
            // you can disable it by finding and disabling the script component
            PlayerControl playerMovement = GetComponent<PlayerControl>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }
// <<<<<<< feature/analytics

            // sets up for the next level
            curLevel += 1;
            UnityEngine.Debug.Log("curLevel: " + curLevel);

            // resets the game to level 1 after the player completes all the levels
            // **should eventually be changed to return to start screen?
            if (curLevel >= SceneManager.sceneCountInBuildSettings)
            {
                curLevel = 0;
            }
// =======
//             string currentSceneName = SceneManager.GetActiveScene().name;
//             if(string.Equals("Level-1",currentSceneName))
//                 curLevel = "Level-2";
//             else if(string.Equals("Level-2",currentSceneName))
//                 curLevel = "Level-3";
//             else if(string.Equals("Level-3",currentSceneName))
//                 curLevel = "Level-4";
//             else
//                 curLevel = "Level-1";
// >>>>>>> master

            StartCoroutine(LoadLevel());
        }

        if (collision.gameObject.CompareTag("Monster") && !alreadyWon)
        {
            // Display the victory screen
            popupController.ShowPopup("You Died! Press Enter to retry the level.");
            popupCanvas.enabled = true;
            UIDocument_pause.SetActive(false);

            // Disable the player movement
            // Assuming you have a script controlling the player's movement,
            // you can disable it by finding and disabling the script component
            PlayerControl playerMovement = GetComponent<PlayerControl>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }

            // update the counter for died during level 1 and subsequently update the database
            if (string.Equals("Level-1", currentSceneName))
            {
                Analytics.playerData.timesRetried[0] += 1;
                Analytics.updateDatabase();
            }

            StartCoroutine(LoadLevel());
        }

        if (collision.gameObject.CompareTag("Coin"))
        {

            // Handle collision with the "Target" object

            // Handle collision logic here
            UnityEngine.Debug.Log("Collision occured");
            // Destroy the collided object
            Destroy(collision.gameObject);
            // Refill flashlight battery
            flashlightControl.RefillFlashlightBattery();
            pointLight.enabled = false;
        }
    }

    IEnumerator LoadLevel()
    {
        // Wait until return key is pressed
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }

        stopWatch.Restart();
        completedLevel = false; // sets for upcoming/reloaded level

        SceneManager.LoadScene(curLevel);
    }

}