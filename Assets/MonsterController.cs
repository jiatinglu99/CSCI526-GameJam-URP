using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    public static int velo = 50;
    private static int maxCount = 2000;
    private int direction = 0;
    private int count;
    private int rnd;

    private Rigidbody rb;
    public GameObject popUpPanel;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = maxCount + 1;
        rnd = Random.Range(0, maxCount);
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (count > rnd)
        {
            SwitchDirection(direction);
        }

        count += 1;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            // Handle collision with the "Target" object
            SwitchDirection(direction);
            // Handle collision logic here
            Debug.Log("Monster collision occured");

        }

        if (collision.gameObject.CompareTag("Wall Collision"))
        {
            // Handle collision with the "Target" object

            // Handle collision logic here
            Debug.Log("Collision occured with player");
            // Destroy the Player

            //Comment for testing
            Destroy(collision.gameObject);
            Application.Quit();
            // ShowQuitPopUp();
        }
    }


    void SwitchDirection(int oldDir)
    {
        count = 0;
        float rndDirection = Random.Range(0, maxCount);
        direction = (int)System.Math.Floor(4 * rndDirection / maxCount);
        if (direction == oldDir)
        {
            direction = (oldDir + 1) % 4;
        }
        // Debug.Log("Direction: " + direction);
        switch (direction)
        {
            case 0: rb.velocity = new Vector3(velo, 0, 0); break;
            case 1: rb.velocity = new Vector3(-velo, 0, 0); break;
            case 2: rb.velocity = new Vector3(0, 0, velo); break;
            default: rb.velocity = new Vector3(0, 0, -velo); break;
            //default: rb.velocity = new Vector3(0, 0, 0); break;
        }

    }

}
