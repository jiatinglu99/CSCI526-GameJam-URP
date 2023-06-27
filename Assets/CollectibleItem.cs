using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the player collides with this object, add it to player inventory
        if (other.gameObject.name == "Player")
        {
            // add this object to the player's inventory
            other.gameObject.GetComponent<PlayerInventory>().AddItemToInventory(gameObject);
            // deactivate this object
            gameObject.SetActive(false);
        }
    }
}
