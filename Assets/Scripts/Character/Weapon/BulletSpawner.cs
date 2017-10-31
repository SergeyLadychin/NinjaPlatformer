using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class BulletSpawner : Weapon
{
    public GameObject bulletPrefab;

    public override float OnFire()
    {
        animator.SetBool("Throw", true);
        return cooldown;
    }

    public override void Fire()
    {
        var bulletObject = Instantiate(bulletPrefab, transform.position, transform.rotation);
        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.flyDirection = controller.GetFacingDirection();
        
        Orient(bullet);
        animator.SetBool("Throw", false);
    }

    private void Orient(Bullet bullet)
    {
        var v = bullet.transform.localScale;
        v.x *= bullet.flyDirection.x;
        bullet.transform.localScale = v;
    }
}

