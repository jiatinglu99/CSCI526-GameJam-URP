using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 10.0f;
    [SerializeField] private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
        movement = movement.normalized * speed * Time.deltaTime;

        // rotate the player to face the moving direction
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
            transform.Translate(-movement, Space.World);
        }

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
        }
        
    }
}
