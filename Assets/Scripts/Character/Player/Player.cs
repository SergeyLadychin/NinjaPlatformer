using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public override void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0 && !GameManager.GetInstance().godModeEnabled)
        {
            controller.SetSimulated(false);
            animator.SetTrigger("Die");
            animator.SetBool("Dead", true);
            characterState.enabled = false;
            EventManager.TriggerEvent(Constants.PlayerDeathEvent);

            StartCoroutine(AfterPlayerDeath());
        }
    }

    private IEnumerator AfterPlayerDeath()
    {
        yield return new WaitForSeconds(GameManager.GetInstance().delayAfterPlayerDeath);
        GameManager.GetInstance().ReloadCurrentLevel();
    }
}
