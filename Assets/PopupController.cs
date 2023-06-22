using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PopupController : MonoBehaviour
{
    public Canvas popupCanvas;
    public GameObject popupPanel;
    public Text popupText;
    public Text popupDesc;

    // public void Start()
    // {
    //     popupText.text = "hello";
    // }

    public void ShowPopup(string message)
    {
        Debug.Log(message);
        popupText.text = message;
        // popupCanvas.gameObject.SetActive(true);
    }

    public void HidePopup()
    {
        // popupCanvas.gameObject.SetActive(false);
    }
}
