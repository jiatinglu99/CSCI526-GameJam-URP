using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseHandler : MonoBehaviour
{
    private void onEnable()
    {
        Debug.Log("PauseHandler.onEnable()");
        var root = GetComponent<UIDocument>().rootVisualElement;       
        var buttonPause = root.Q<Button>("Pause");
        buttonPause.RegisterCallback<ClickEvent>(ev => pauseGame());
    }

    private void pauseGame()
    {
        Debug.Log("PauseHandler.pauseGame()");
    }
}
