using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterItem : MonoBehaviour
{
    private Material filterMaterial;

    void Start()
    {
        // find filter material which is a child of this object(lens)
        filterMaterial = gameObject.transform.GetChild(0).GetComponent<Renderer>().material;
    }
    private void OnTriggerEnter(Collider other)
    {
        // if the player collides with this object, add it to player inventory
        if (other.gameObject.name == "Player")
        {
            // add this object to the player's inventory
            other.gameObject.GetComponent<FlashlightFilterChange>().ChangeLightFilter(filterMaterial);
            // deactivate this object
            // gameObject.SetActive(false);
        }
    }
}
