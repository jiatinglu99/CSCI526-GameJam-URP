using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightControl : MonoBehaviour
{
    private GameObject flashLight;
    private GameObject cheatLight;
    private GameObject pointLight;
    [SerializeField] public float flashLightDrainSpeed = 0.005f;
    public float flashLightOffThreshold = 500.0f;
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
        Debug.Log(flashLightIntensityFull);
        Debug.Log(pointLightIntensityFull);

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

    void FixedUpdate()
    {
        // flashlight drains battery over time
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
    }

    public  void RefillFlashlightBattery()
    {
        flashLight.GetComponent<Light>().intensity = flashLightIntensityFull;
        pointLight.GetComponent<Light>().intensity = pointLightIntensityFull;
    }
}
