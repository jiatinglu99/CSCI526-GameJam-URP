using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    private int levelNum;
    private void OnEnable()
    {
        Debug.Log("StartUI.OnEnable()");
        var root = GetComponent<UIDocument>().rootVisualElement;
        var startButton = root.Q<Button>("ButtonStart");
        var levelsButton = root.Q<Button>("ButtonReset");
        startButton.RegisterCallback<ClickEvent>(ev => StartGame());
        levelsButton.RegisterCallback<ClickEvent>(ev => ResetLevel()); 

        var levelLabel = root.Q<Label>("LevelLabel");
        levelNum = PlayerPrefs.HasKey("LatestLevel") ? PlayerPrefs.GetInt("LatestLevel") : 1;
        levelLabel.text = "LEVEL-" + levelNum.ToString();

        if (levelNum != 1)
        {
            // Change "start" to "continue
            startButton.text = "CONTINUE";
        }
        else{
            startButton.text = "START";
        }
    }

    private void StartGame()
    {
        Debug.Log("StartUI.StartGame()");
        SceneManager.LoadScene(levelNum);
    }

    private void ResetLevel()
    {
        Debug.Log("StartUI.ResetLevel()");
        PlayerPrefs.SetInt("LatestLevel", 1);
        OnEnable();
    }
}
