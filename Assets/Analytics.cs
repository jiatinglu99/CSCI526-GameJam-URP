using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using System;

[System.Serializable]
public class PlayerData
{
    public string name;
    public int score;

}

public class Analytics : MonoBehaviour
{
    private IEnumerator Start()
    {

        Debug.Log("OVER HERE");
        PlayerData playerData = new PlayerData();
        playerData.name = "John";
        playerData.score = 100;

        string json = JsonUtility.ToJson(playerData);

        string URL = "https://jomandterry-3569d.firebaseio.com";
        string key = "/example2";

        using (var uwr = new UnityWebRequest(URL + key + ".json", "POST"))
        {

            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            using UploadHandlerRaw uploadHandler = new UploadHandlerRaw(jsonToSend);
            uwr.uploadHandler = uploadHandler;
            uwr.downloadHandler = new DownloadHandlerBuffer();
            uwr.disposeUploadHandlerOnDispose = true;
            uwr.disposeDownloadHandlerOnDispose = true;
            uwr.SetRequestHeader("Content-Type", "application");
            uwr.timeout = 5;
            // Send the request then wait here till it returns
            yield return uwr.SendWebRequest();
            Debug.Log("HERE!");

            //string timestamp = GlobalAnalysis.getTimeStamp();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error While Sending: " + uwr.error);
            }
            else
            {
                Debug.Log("Data Received: " + uwr.downloadHandler.text);
            }

        }
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
}
