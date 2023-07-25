using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightFilterChange : MonoBehaviour
{
    private GameObject flashLight;
    private static Color whiteLight = new Color32(255, 255, 255, 255);
    private static Color blueLight = new Color32(0, 100, 255, 255);
    private static Color redLight = new Color32(255, 0, 0, 255);

    public Material glowingWhiteMaterial;
    public Material glowingBlueMaterial;
    public Material glowingRedMaterial;

    // dictionary of material to colors
    private Dictionary<string, Color> materialToColor = new Dictionary<string, Color>();

    // Start is called before the first frame update
    void Start()
    {
        //  load all color & material pairs into dictionary
        materialToColor.Add(glowingWhiteMaterial.name + " (Instance)", whiteLight);
        materialToColor.Add(glowingBlueMaterial.name + " (Instance)", blueLight);
        materialToColor.Add(glowingRedMaterial.name + " (Instance)", redLight);
        // print out dictionary
        foreach (KeyValuePair<string, Color> kvp in materialToColor)
        {
            UnityEngine.Debug.Log("Key = " + kvp.Key + ", Value = " + kvp.Value);
        }

        // find flashlight 
        flashLight = GameObject.Find("/Player/Flashlight");
    }

    public void ChangeLightFilter(Material filterMaterial)
    {
        UnityEngine.Debug.Log("Changing flashlight color to " + materialToColor[filterMaterial.name]);
        // TODO: Set flashlight to color according to dictionary
        flashLight.GetComponent<Light>().color = materialToColor[filterMaterial.name];
    }
}
