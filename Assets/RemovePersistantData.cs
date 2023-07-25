using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePersistantData : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll(); // Delete all PlayerPrefs data when the game is closed.
    }
}
