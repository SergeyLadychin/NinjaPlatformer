using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public float destructionDelay;

    void OnEnable()
    {
        EventManager.StartListen(Constants.PlayerDeathEvent, PlayerDead);
    }

    void OnDisable()
    {
        EventManager.StopListen(Constants.PlayerDeathEvent, PlayerDead);
    }

    public override void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            controller.SetSimulated(false);
            animator.SetTrigger("Dead");
            characterState.enabled = false;
            StartCoroutine(DestructionDelay());
        }
    }

    private IEnumerator DestructionDelay()
    {
        yield return new WaitForSeconds(destructionDelay);
        Destroy(gameObject);
    }

    private void PlayerDead()
    {
        characterState.enabled = false;
    }
}
