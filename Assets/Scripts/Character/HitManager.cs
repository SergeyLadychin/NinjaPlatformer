using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    private static HitManager instance;

    public ObjectGetter ammoHitGetter;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static void SpawnHitEffect(Vector3 position, Vector3 direction, EffectType type)
    {
        if (instance == null)
            return;

        var hitAnimation = instance.ammoHitGetter.Get<HitAnimation>(position, Quaternion.identity);
        hitAnimation.Init(type, direction);
    }
}

public enum EffectType
{
    Blood,
    SolidObjectHit,
    BombExplosion,
    BossExplosion
}
