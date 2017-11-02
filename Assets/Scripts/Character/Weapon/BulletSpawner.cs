using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class BulletSpawner : Weapon
{
    public ObjectGetter bulletGetter;
    public ObjectGetter ammoHitGetter;

    public override float OnFire()
    {
        animator.SetBool("Throw", true);
        return cooldown;
    }

    public override void Fire()
    {
        Bullet bullet = bulletGetter.Get<Bullet>(transform.position, transform.rotation);

        if (bulletGetter.IsFromPool())
        {
            bullet.SetDisableFunc(obj => obj.SetActive(false));
        }
        else
        {
            bullet.SetDisableFunc(Destroy);
        }

        bullet.flyDirection = controller.GetFacingDirection();
        bullet.ammoHitGetter = ammoHitGetter;

        Orient(bullet);
        animator.SetBool("Throw", false);
    }

    public override bool IsAvaliable()
    {
        return true;
    }

    private void Orient(Bullet bullet)
    {
        var v = bullet.transform.localScale;
        v.x *= bullet.flyDirection.x;
        bullet.transform.localScale = v;
    }
}

