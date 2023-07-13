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
        // check the other object is the player
        if (other.gameObject.tag != "Player")
        {
            return;
        }
        Analytics.playerData.cornersVisited[curLevel.ToString() + "-" + currCorner.ToString()] += 1;

        UnityEngine.Debug.Log("Corner Hit - Trigger " + currCorner.ToString());
        Analytics.updateDatabase();
    }
}
