using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class FlashlightControl : MonoBehaviour
{
    private GameObject flashLight;
    private GameObject cheatLight;
    private GameObject pointLight;
    private Label myLabel;
    [SerializeField] 
    public GameObject UIDocument_pause;
    public float flashLightDrainSpeed = 0.0006f; // 0.00065f;
    public float flashLightOffThreshold = 800.0f; // 800.0f;
    private float flashLightIntensityFull;
    private float pointLightIntensityFull;

    // Start is called before the first frame update
    void Start()
    {

        flashLight = GameObject.Find("/Player/Flashlight");
        pointLight = GameObject.Find("/Player/Flashlight/PointLight");
        cheatLight = GameObject.Find("/Player/CheatLight");

        flashLightIntensityFull = flashLight.GetComponent<Light>().intensity;
        pointLightIntensityFull = pointLight.GetComponent<Light>().intensity;

        // flashlight is on by default
        flashLight.SetActive(true);
        // cheat light is off by default
        cheatLight.SetActive(false);

        // Debug.Log("FlashlightControl.Start() "+myLabel.text);
    }

    // Update is called once per frame
    void Update()
    {
        // if space is pressed, toggle the flashlight
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // temporarily disabled for playtesting
            // flashLight.SetActive(!flashLight.activeSelf);
        }
        // if c is pressed, toggle the cheat light
        if (Input.GetKeyUp(KeyCode.C))
        {
            cheatLight.SetActive(!cheatLight.activeSelf);
        }
    }

    void FixedUpdate()
    {
        // flashlight drains battery over time if active
        if (flashLight.activeSelf)
        {
            DrainFlashlightBattery();
        }
    }

    void DrainFlashlightBattery()
    {

        // Debug.Log("DrainFlashlightBattery "+flashLight.GetComponent<Light>().intensity/flashLightIntensityFull);
        if (flashLight.GetComponent<Light>().intensity > flashLightOffThreshold)
        {
            flashLight.GetComponent<Light>().intensity -= flashLightDrainSpeed * flashLightIntensityFull;
            pointLight.GetComponent<Light>().intensity -= flashLightDrainSpeed * pointLightIntensityFull;
        }
        else
        {
            flashLight.GetComponent<Light>().intensity = 0;
            pointLight.GetComponent<Light>().intensity = 0;
        }
        // Debug.Log("DrainFlashlightBattery "+myLabel.text);
    }

    public void RefillFlashlightBattery()
    {
        flashLight.GetComponent<Light>().intensity = flashLightIntensityFull;
        pointLight.GetComponent<Light>().intensity = pointLightIntensityFull;
    }

    public int GetFlashlightBatteryLevel()
    {
        return (int)(Math.Max(0f, (flashLight.GetComponent<Light>().intensity - flashLightOffThreshold)/(flashLightIntensityFull - flashLightOffThreshold) * 100.0f));
    }
}
