using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseHandler : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("PauseHandler.onEnable()");
        var root = GetComponent<UIDocument>().rootVisualElement;
        var InnerPauseConatiner = root.Q<VisualElement>("InnerPauseContainer");
        InnerPauseConatiner.style.display = DisplayStyle.None;

        var buttonPause = root.Q<Button>("Pause");
        buttonPause.RegisterCallback<ClickEvent>(ev => pauseGame());

        var buttonResume = root.Q<Button>("ButtonResume");
        buttonResume.RegisterCallback<ClickEvent>(ev => resumeGame());

        var buttonQuit = root.Q<Button>("ButtonQuit");
        buttonQuit.RegisterCallback<ClickEvent>(ev => quitGame());
    }

    private void pauseGame()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var InnerPauseConatiner = root.Q<VisualElement>("InnerPauseContainer");
        InnerPauseConatiner.style.display = DisplayStyle.Flex;
        Debug.Log("PauseHandler.pauseGame()");
        Time.timeScale = 0f;
    }

    private void resumeGame()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var InnerPauseConatiner = root.Q<VisualElement>("InnerPauseContainer");
        InnerPauseConatiner.style.display = DisplayStyle.None;
        Debug.Log("PauseHandler.resumeGame()");
        Time.timeScale = 1f;
    }

    private void quitGame()
    {
        Debug.Log("quit game!");
        
        SceneManager.LoadScene("Start");
    }
}