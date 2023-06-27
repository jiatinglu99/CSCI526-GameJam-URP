using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Camera mainCamera;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float originalFieldOfView;

    private void Awake()
    {
        mainCamera = Camera.main;
        originalPosition = mainCamera.transform.position;
        originalRotation = mainCamera.transform.rotation;
        originalFieldOfView = mainCamera.fieldOfView;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CameraController.Start()");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ZoomOutCamera()
    {
        Debug.Log("CameraController.ZoomOutCamera()");
        // Set the desired zoom out position, rotation, and field of view
        // Vector3 zoomOutPosition = new Vector3(0f, 10f, -10f);
        // Quaternion zoomOutRotation = Quaternion.Euler(30f, 0f, 0f);
        // float zoomOutFieldOfView = 60f;

        // mainCamera.transform.position = zoomOutPosition;
        // mainCamera.transform.rotation = zoomOutRotation;
        // mainCamera.fieldOfView = zoomOutFieldOfView;
    }

    public void RestoreCameraSettings()
    {
        mainCamera.transform.position = originalPosition;
        mainCamera.transform.rotation = originalRotation;
        mainCamera.fieldOfView = originalFieldOfView;
    }

}
