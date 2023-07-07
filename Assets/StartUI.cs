using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("StartUI.OnEnable()");
        var root = GetComponent<UIDocument>().rootVisualElement;
        var startButton = root.Q<Button>("ButtonStart");
        var levelsButton = root.Q<Button>("ButtonLevels");
        var aboutButton = root.Q<Button>("ButtonAbout");
        startButton.RegisterCallback<ClickEvent>(ev => StartGame());
        // TODO: levelsButton.RegisterCallback<ClickEvent>(ev => Levels()); 
        // TODO: aboutButton.RegisterCallback<ClickEvent>(ev => About());
    }

    private void StartGame()
    {
        Debug.Log("StartUI.StartGame()");
        SceneManager.LoadScene("Level-1");
    }
}
