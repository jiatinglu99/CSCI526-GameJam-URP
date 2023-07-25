using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    protected int collidingObjects = 0;
    protected List<GameObject> targetWallsList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        // find all walls with the same material as the button
        targetWallsList.Clear();
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall Collision");
        foreach (GameObject wall in walls)
        {
            if (wall.GetComponent<Renderer>().material.ToString() == GetComponent<Renderer>().material.ToString())
            {
                targetWallsList.Add(wall);
                UnityEngine.Debug.Log("Found wall with same material as button");
            }
        }
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
                FlipTargetState();
            }
        }
    }

    public virtual void FlipTargetState()
    {
        // targetObject.SetActive(!targetObject.activeSelf);
        // flip state for all tag "wall collision" objects with the same material as button
        foreach (GameObject wall in targetWallsList)
        {
            wall.SetActive(!wall.activeSelf);
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
