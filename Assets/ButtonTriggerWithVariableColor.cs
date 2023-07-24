using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggerWithVariableColor : ButtonTrigger
{
    private static Color defaultColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private static Color blueColor = new Color(0.0f, 0.2f, 1.0f, 1.0f);
    private Color currentColor = defaultColor;

    void Start()
    {
        // pass
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    public override void OnTriggerEnter(Collider other)
    {
        // check if the object is the player
        if (other.gameObject.name == "Player")
        {
            collidingObjects++;
            if (collidingObjects == 1 && currentColor == blueColor)
            {
                FlipTargetState();
            }
        }
        else if (other.gameObject.name == "Flashlight")
        {
            // Change color to flashlight color
            Material mat = GetComponent<Renderer>().material;
            mat.SetColor("_EmissionColor", other.gameObject.GetComponent<Light>().color);

            if (other.gameObject.GetComponent<Light>().color == blueColor)
            {
                currentColor = blueColor;
            }
            else
            {
                currentColor = defaultColor;
            }
            // TODO: need to update targetWallsList upon color change
        }
    }
}
