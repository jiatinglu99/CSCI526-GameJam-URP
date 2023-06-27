using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
     protected int collidingObjects = 0;
    public GameObject targetObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    public virtual void OnTriggerEnter(Collider other)
    {
        // check if the object is the player
        if (other.gameObject.name == "Player")
        {
            collidingObjects++;
            if (collidingObjects == 1)
            {
                targetObject.SetActive(!targetObject.activeSelf);
            }
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        // check if the object is the player
        if (other.gameObject.name == "Player")
        {
            collidingObjects--;
            if (collidingObjects == 0)
            {
                // targetObject.SetActive(true);
            }
        }
    }
}
