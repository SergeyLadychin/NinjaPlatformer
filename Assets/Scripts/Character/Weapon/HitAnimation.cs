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

    public void Init(ObjectHit targetHit, Vector2 hitDirection)
    {
        Orient(hitDirection);
        animator.SetFloat("TargetHit", targetHit == ObjectHit.Body ? 0.0f : 1.0f);
    }

    public void EndOfAnimation()
    {
        gameObject.SetActive(false);
    }

    private void Orient(Vector2 hitDirection)
    {
        var v = transform.localScale;
        v.x *= hitDirection.x;
        transform.localScale = v;
    }
}

public enum ObjectHit
{
    SolidObject,
    Body
}
