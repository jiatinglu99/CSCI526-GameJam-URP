using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using System;

public class CornerAnalytic : MonoBehaviour
{
    private int curLevel;

    [SerializeField]
    public int currCorner;

    // Start is called before the first frame update
    void Start()
    {
        curLevel = SceneManager.GetActiveScene().buildIndex;
        UnityEngine.Debug.Log("Corner Analytics Level " + curLevel.ToString() + " Corner " + currCorner.ToString() + " Start()");
    }

    private void OnTriggerEnter(Collider other)
    {
        Analytics.playerData.cornersVisited[curLevel, currCorner] += 1;

        Analytics.updateDatabase();
        UnityEngine.Debug.Log("Corner Hit - Trigger");
    }
}
