using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    private int collidingObjects = 0;
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
    private void OnTriggerEnter(Collider other)
    {
        collidingObjects++;
        if (collidingObjects == 1)
        {
            targetObject.SetActive(!targetObject.activeSelf);
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     collidingObjects--;
    //     if (collidingObjects == 0)
    //     {
    //         targetObject.SetActive(true);
    //     }
    // }
}
