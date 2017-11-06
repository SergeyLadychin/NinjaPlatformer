using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnimation : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Init(EffectType hitType, Vector2 hitDirection)
    {
        Orient(hitDirection);

        float targetHit = 0.0f;
        switch (hitType)
        {
            case EffectType.Blood:
                targetHit = 0.0f;
                break;
            case EffectType.SolidObjectHit:
                targetHit = 1.0f;
                break;
            case EffectType.BombExplosion:
                targetHit = 2.0f;
                break;
            case EffectType.BossExplosion:
                targetHit = 3.0f;
                break;

        }
        animator.SetFloat("TargetHit", targetHit);
    }

    public void EndOfAnimation()
    {
        gameObject.SetActive(false);
    }

    private void Orient(Vector2 hitDirection)
    {
        var rotation = Quaternion.FromToRotation(Vector3.right, hitDirection);
        transform.rotation = rotation;
    }
}
