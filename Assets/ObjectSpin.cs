using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpin : MonoBehaviour
{
    public float spinSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        // spin
        transform.Rotate(0.0f, spinSpeed, 0.0f, Space.World);
    }
}
