using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggerWithVariableColor : ButtonTrigger
{
    private static Color whiteLight = new Color32(255, 255, 255, 255);
    private static Color blueLight = new Color32(0, 100, 255, 255);
    private static Color redLight = new Color32(255, 30, 0, 255);
    private Color currentColor = whiteLight;

    public Material glowingWhiteMaterial;
    public Material glowingBlueMaterial;
    public Material glowingRedMaterial;

    private bool isSet = false;

    // dictionary of colors to materials
    private Dictionary<string, Material> colorToMaterial = new Dictionary<string, Material>();

    void Start()
    {
        // load all color & material pairs into dictionary
        colorToMaterial.Add(whiteLight.ToString(), glowingWhiteMaterial);
        colorToMaterial.Add(blueLight.ToString(), glowingBlueMaterial);
        colorToMaterial.Add(redLight.ToString(), glowingRedMaterial);

        // set to default color
        currentColor = whiteLight;
        // set material to default color
        GetComponent<Renderer>().material = colorToMaterial[currentColor.ToString()];
        UnityEngine.Debug.Log("Button color set to " + currentColor);
        UnityEngine.Debug.Log("Button material set to " + GetComponent<Renderer>().material);
        UnityEngine.Debug.Log("Button material set to " + GetComponent<Renderer>().material.ToString());
        UnityEngine.Debug.Log("Dict material is " + colorToMaterial[currentColor.ToString()].ToString());
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    public override void OnTriggerEnter(Collider other)
    {
        // check if the object is the player
        if (other.gameObject.name == "Player")
        {
            if (currentColor != whiteLight)
            {
                FlipTargetState();
            }
        }
        else if (other.gameObject.name == "Flashlight")
        {
            if (isSet)
            {
                return;
            }
            // set material to the same color represented by the light
            // mat.SetColor("_EmissionColor", other.gameObject.GetComponent<Light>().color);

            if (other.gameObject.GetComponent<Light>().color == blueLight)
            {
                UnityEngine.Debug.Log("Blue light detected");
                currentColor = blueLight;
                GetComponent<Renderer>().material = colorToMaterial[currentColor.ToString()];
                isSet = true;
            }
            else if (other.gameObject.GetComponent<Light>().color == whiteLight)
            {
                UnityEngine.Debug.Log("White light detected");
                currentColor = whiteLight;
                GetComponent<Renderer>().material = colorToMaterial[currentColor.ToString()];
            }
            else if (other.gameObject.GetComponent<Light>().color == redLight)
            {
                UnityEngine.Debug.Log("Red light detected");
                currentColor = redLight;
                GetComponent<Renderer>().material = colorToMaterial[currentColor.ToString()];
                isSet = true;
            }
            else
            {
                UnityEngine.Debug.Log("Unknown light detected");
                UnityEngine.Debug.Log(other.gameObject.GetComponent<Light>().color);
                UnityEngine.Debug.Log(blueLight);
            }
            // update targetWallsList upon color change
            UpdateWallList();
        }
    }

    private void UpdateWallList()
    {
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
}
