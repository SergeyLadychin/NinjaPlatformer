using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Weapon
{
    public GameObject bulletPrefab;

    public override float Fire()
    {
        var bulletObject = Instantiate(bulletPrefab, transform.position, transform.rotation);
        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.flyDirection = controller.GetFacingDirection();
        return cooldown;
    }
}

