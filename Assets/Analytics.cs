using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using System;
using Proyecto26;
using Newtonsoft.Json;

[System.Serializable]
public class PlayerData
{
    public int userID;
    public long timeStamp;
    public int highestCompletedLevel;
    public float[] timeSpent = new float[30]; // change number based on how many levels there are
    public int[] timesRetried = new int[30]; // ^^^
    // public int[,] cornersVisited = new int[8,4];
    public Dictionary<String, int> cornersVisited = new Dictionary<String, int>();
}

public class Analytics : MonoBehaviour
{
    public static PlayerData playerData = new PlayerData();

    private IEnumerator Start()
    {
        System.Random random = new System.Random();
        Debug.Log("OVER HERE");
        
        playerData.userID = random.Next(100000, 1000000); // generates a six digit value between 100,000 and 999,999
        playerData.timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(); // probably could just use this as a unique userID instead
        playerData.highestCompletedLevel = 0;
        playerData.timeSpent[0] = 0.0f;
        playerData.timesRetried[0] = 0;

        for (int row = 0; row < 8; row++)
        {
            for (int column = 0; column < 4; column++)
            {
                // Assign a value based on row and column indices
                playerData.cornersVisited[row.ToString() + "-" + column.ToString()] = 0;

                string json = JsonUtility.ToJson(playerData);
            }
        }

        RestClient.Post("https://jomandterry-3569d-default-rtdb.firebaseio.com/.json", playerData);
        yield break;
        //string URL = "https://jomandterry-3569d.firebaseio.com";
        //string key = "/example2";

        //using (var uwr = new UnityWebRequest(URL + key + ".json", "POST"))
        //{

        //    byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        //    using UploadHandlerRaw uploadHandler = new UploadHandlerRaw(jsonToSend);
        //    uwr.uploadHandler = uploadHandler;
        //    uwr.downloadHandler = new DownloadHandlerBuffer();
        //    uwr.disposeUploadHandlerOnDispose = true;
        //    uwr.disposeDownloadHandlerOnDispose = true;
        //    uwr.SetRequestHeader("Content-Type", "application");
        //    uwr.timeout = 5;
        //    // Send the request then wait here till it returns
        //    yield return uwr.SendWebRequest();
        //    Debug.Log("HERE!");

        //    //string timestamp = GlobalAnalysis.getTimeStamp();

        //    if (uwr.result != UnityWebRequest.Result.Success)
        //    {
        //        Debug.Log("Error While Sending: " + uwr.error);
        //    }
        //    else
        //    {
        //        Debug.Log("Data Received: " + uwr.downloadHandler.text);
        //    }

        //}


        //UnityWebRequest request = UnityWebRequest.Post(databaseURL + endpoint, json);
        //request.SetRequestHeader("Content-Type", "application");

        //yield return request.SendWebRequest();

        //if (request.result == UnityWebRequest.Result.Success)
        //{
        //    Debug.Log("POST request successful");
        //}
        //else
        //{
        //    Debug.LogError("Error sending POST request: " + request.error);
        //}
    }

    public static void updateDatabase()
    {
        Debug.Log("Updating database");
        string json = JsonConvert.SerializeObject(playerData);
        RestClient.Post("https://jomandterry-3569d-default-rtdb.firebaseio.com/.json", playerData);
        UnityEngine.Debug.Log(json);
    }
}
