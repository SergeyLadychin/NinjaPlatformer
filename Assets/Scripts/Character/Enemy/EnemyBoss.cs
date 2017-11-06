using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    public override void TakeDamage(int damageAmount, Vector2 hitPosition, Vector2 hitDirection)
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
}
