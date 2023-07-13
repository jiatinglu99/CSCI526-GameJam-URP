using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildObjectSpin : MonoBehaviour
{
    public float spinSpeed = 2.0f;
    void FixedUpdate()
    {
        // only spin the child object under this parent
        foreach (Transform child in transform)
        {
            child.Rotate(0.0f, spinSpeed, 0.0f, Space.World);
        }
    }
}
