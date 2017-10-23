using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpBlade : Weapon
{
    public float attackRadius;
    public LayerMask whoIsEnemy;

    public override float Fire()
    {
        Debug.DrawLine(transform.position, transform.position + controller.GetFacingDirection() * attackRadius);
        var colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, whoIsEnemy);
        for (int i = 0; i < colliders.Length; i++)
        {
            var enemy = colliders[i].gameObject.GetComponent<Enemy>();
            enemy.TakeDamage();
        }

        return cooldown;
    }
}
