using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Weapon
{
    public float lifeTime;
    public ObjectGetter bulletGetter;

    public override float OnFire()
    {
        animator.SetTrigger("Shoot");
        return cooldown;
    }

    public override void Fire()
    {
        var flyDirection = controller.GetFacingDirection();
        var rotation = Quaternion.FromToRotation(Vector3.right, flyDirection);
        Bullet bullet = bulletGetter.Get<Bullet>(transform.position, rotation);

        if (bulletGetter.IsFromPool())
        {
            bullet.Init(flyDirection, lifeTime, obj => obj.SetActive(false));
        }
        else
        {
            bullet.Init(flyDirection, lifeTime, Destroy);
        }
    }

    public override bool IsAvaliable()
    {
        return true;
    }
}

