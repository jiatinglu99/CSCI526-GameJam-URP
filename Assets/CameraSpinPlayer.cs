using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpinPlayer : MonoBehaviour
{
    // camera slowly spins around the player at 45 degrees angle
    private Transform player;
    public float cameraDistance = 100.0f;
    public float rotateSpeed = 0.2f;
    private float t = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // the following code is made with help from Github Copilot
        // camera spins around the player at cameraAngle degree with cameraDistance distance
        t += Time.deltaTime * rotateSpeed;
        float x = Mathf.Cos(t) * cameraDistance;
        float z = Mathf.Sin(t) * cameraDistance;
        transform.position = player.position + new Vector3(x, cameraDistance, z);
        transform.LookAt(player.position);
    }
}
