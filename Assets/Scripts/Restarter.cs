using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
    public bool usePlayerDeathDelay;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            if (usePlayerDeathDelay)
            {
                StartCoroutine(RestartWithDelay());
            }
            else
            {
                Restart();
            }
        }
    }

    private IEnumerator RestartWithDelay()
    {
        yield return new WaitForSeconds(GameManager.GetInstance().delayAfterPlayerDeath);
        Restart();
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
