using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private bool playerInside;

    public string nextLevelName;

    void Update()
    {
        if (playerInside)
        {
            if (Input.GetAxis("Vertical") > Constants.axisThreshold)
            {
                playerInside = false;
                GameManager.GetInstance().GoToNextLevel(nextLevelName);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            playerInside = false;
        }
    }
}
