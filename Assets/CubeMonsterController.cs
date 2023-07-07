using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeMonsterController : MonoBehaviour
{
    public float rollSpeed = 5000000.0f;
    public bool rolling = true;
    Vector3 anchor;
    Vector3 axis;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     anchor = transform.position + new Vector3(0,-1,1) * 20.0f;
    //     axis = Vector3.Cross(Vector3.up, new Vector3(0,0,1));
    //     if (rolling == true) {
    //         for (int i = 0; i < (90 / rollSpeed); i++) {
    //             transform.RotateAround(anchor, axis, rollSpeed);
    //         }
    //     }
    // }

    void Update()
    {
        if (rolling) {
            Vector3 torque = new Vector3(0, 0, rollSpeed * Time.deltaTime);
            rb.AddTorque(torque, ForceMode.VelocityChange);
        }
    }
}
