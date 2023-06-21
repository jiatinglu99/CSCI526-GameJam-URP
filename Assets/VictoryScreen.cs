using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public GameObject victoryScreen; // Reference to the victory screen UI object

    private void OnTriggerEnter(Collider other)
    {
        // Display the victory screen
        victoryScreen.SetActive(true);

        // Disable the player movement
        // Assuming you have a script controlling the player's movement,
        // you can disable it by finding and disabling the script component
        PlayerControl playerMovement = GetComponent<PlayerControl>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }
}