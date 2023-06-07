using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightControl : MonoBehaviour
{
    private GameObject flashLight;
    private GameObject cheatLight;
    // Start is called before the first frame update
    void Start()
    {
        flashLight = GameObject.Find("/Player/Flashlight");
        cheatLight = GameObject.Find("/Player/CheatLight");
        // flashlight is on by default
        flashLight.SetActive(true);
        // cheat light is off by default
        cheatLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if space is pressed, toggle the flashlight
        if (Input.GetKeyUp(KeyCode.Space))
        {
            flashLight.SetActive(!flashLight.activeSelf);
        }
        // if c is pressed, toggle the cheat light
        if (Input.GetKeyUp(KeyCode.C))
        {
            cheatLight.SetActive(!cheatLight.activeSelf);
        }
    }
}
