using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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
            bullet.SetDisableFunc(obj => obj.SetActive(false));
        }
        else
        {
            bullet.SetDisableFunc(Destroy);
        }

        bullet.flyDirection = flyDirection;
        bullet.lifeTime = lifeTime;
    }

    public override bool IsAvaliable()
    {
        return true;
    }
}

