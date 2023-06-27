using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Debug.Log("CameraController.Start()");
        // ZoomOutCamera();
    }

    public void ZoomOutCamera()
    {
        Debug.Log("CameraController.ZoomOutCamera()");
        StartCoroutine(AnimateZoomOut());
    }
    private IEnumerator AnimateZoomOut()
    {

        mainCamera = Camera.main;
        float zoomSpeed = 1f;
        float maxZoomOut = 120f;
        float minZoomIn = 10f;
        float targetZoom = maxZoomOut;
        float initialZoom = mainCamera.fieldOfView;
        float t = 0f;
        float duration = 3f; // Duration of the zoom animation (in seconds)
        
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float newFOV = Mathf.Lerp(initialZoom, targetZoom, t);
            mainCamera.fieldOfView = Mathf.Clamp(newFOV, minZoomIn, maxZoomOut);
            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float newFOV = Mathf.Lerp(targetZoom, initialZoom, t);
            mainCamera.fieldOfView = Mathf.Clamp(newFOV, minZoomIn, maxZoomOut);
            yield return null;
        }
        
        Debug.Log("CameraController.ZoomOutCamera()");
    }

}
