using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            if (Input.GetAxis("Vertical") > 0.0f)
            {
                GameManager.GetInstance().GoToNextLevel();
            }
        }
    }
}
