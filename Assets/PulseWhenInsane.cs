using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseWhenInsane : MonoBehaviour
{
    public Color32 startColor = new Color32(255, 255, 255, 255);
    public Color32 endColor = new Color32(255, 30, 0, 255);
    private bool isInsane = false;
    private bool pulseOn = false;

    public float pulseInterval = 0.5f;
    private float pulseTimer = 0f;
    private Light pointLight;

    void Start()
    {
        pointLight = GameObject.Find("/Player/PointLight").GetComponent<Light>();
    }

    public void SetInsane(bool insane)
    {
        isInsane = insane;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInsane)
        {
            pulseTimer -= Time.deltaTime;
            if (pulseTimer <= 0)
            {
                pulseOn = !pulseOn;
                pulseTimer = pulseInterval;
            }
            // change material between pulse but lerp it
            if (pulseOn)
            {
                pointLight.color = Color.Lerp(startColor, endColor, pulseTimer / pulseInterval);   
            }
            else
            {
                pointLight.color = Color.Lerp(endColor, startColor, pulseTimer / pulseInterval);
            }
            UnityEngine.Debug.Log("pulse: " + (pulseTimer / pulseInterval));
        }
    }
}
