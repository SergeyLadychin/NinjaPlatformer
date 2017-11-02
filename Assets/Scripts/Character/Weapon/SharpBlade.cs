using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpBlade : Weapon
{
    public float attackRadius;
    public LayerMask whoIsEnemy;

    public override float OnFire()
    {
        animator.SetBool("BladeAttack", true);
        return cooldown;
    }

    public override void Fire()
    {
        Debug.DrawLine(transform.position, transform.position + controller.GetFacingDirection() * attackRadius, Color.green);
        var colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, whoIsEnemy);
        for (int i = 0; i < colliders.Length; i++)
        {
            var enemy = colliders[i].gameObject.GetComponent<Character>();
            if (enemy)
            {
                enemy.TakeDamage(damage);
            }
        }
        animator.SetBool("BladeAttack", false);
    }

    public override bool IsAvaliable()
    {
        return true;
    }
}
