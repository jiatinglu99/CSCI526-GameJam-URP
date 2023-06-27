using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private GameObject flashLight;
    private GameObject activeItem;
    private Color defaultColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private Color blueColor = new Color(0.0f, 0.2f, 1.0f, 1.0f);
    // list of 10 items in the player's inventory
    private List<GameObject> inventory = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        // add flashlight to inventory
        flashLight = GameObject.Find("/Player/Flashlight");
        inventory.Add(flashLight);
        // initialize the remaining 9 slots to null
        for (int i = 0; i < 9; i++)
        {
            inventory.Add(null);
        }
    }

    int FindNextAvailableInventorySlot()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    public void AddItemToInventory(GameObject item)
    {
        int nextAvailableSlot = FindNextAvailableInventorySlot();
        if (nextAvailableSlot != -1)
        {
            inventory[nextAvailableSlot] = item;
            // temporary solution to just switch to the new item
            SwitchToItem(nextAvailableSlot);
        }
    }

    void itemDeactivate(GameObject item)
    {
        // Do nothing for now
    }

    void itemActivate(GameObject item)
    {
        Debug.Log("Activating item: " + item);
        // temporary solution for flashlight color change demo
        if (item == inventory[0])
        {
            // change flashlight color to default
            flashLight.GetComponent<Light>().color = defaultColor;
        }
        else if (item == inventory[1])
        {
            flashLight.GetComponent<Light>().color = blueColor;
        }
    }

    void SwitchToItem(int index)
    {
        if (inventory[index] != null)
        {
            itemDeactivate(activeItem);
            activeItem = inventory[index];
            itemActivate(activeItem);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check key presses for switching items
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SwitchToItem(0);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SwitchToItem(1);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SwitchToItem(2);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            SwitchToItem(3);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            SwitchToItem(4);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            SwitchToItem(5);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            SwitchToItem(6);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            SwitchToItem(7);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            SwitchToItem(8);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            SwitchToItem(9);
        }
    }
}
