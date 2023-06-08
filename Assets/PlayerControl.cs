using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 100.0f;
    public float turnSpeed = 20.0f;
    public float axisLockY = 27.5f;
    private Rigidbody rb;
    private Vector3 lastMovement;

    private FlashlightControl flashlightControl;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        flashlightControl = GetComponent<FlashlightControl>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical   = Input.GetAxis("Vertical");

        Vector3 movement;
        if (moveHorizontal != 0)
        {
            movement = new Vector3(moveHorizontal, 0, 0);
        }
        else
        {
            movement = new Vector3(0, 0, moveVertical);
        }
        movement = movement.normalized * speed * Time.fixedDeltaTime;

        // rotate the player to face the moving direction
        if (movement != Vector3.zero)
        {
            // transform.rotation = Quaternion.LookRotation(movement);
            // transform.Translate(-movement, Space.World);
            Quaternion toRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
            // transform.Translate(-movement, Space.World);
            rb.MovePosition(transform.position - movement);
            lastMovement = movement;
        }

        // prevent effect from collision
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        // lock y axis
        rb.MovePosition(new Vector3(rb.position.x, axisLockY, rb.position.z));

        // Debug.Log(rb.velocity);
    }

    void OnCollisionEnter(Collision collision)
    {
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
}
