using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoHit : MonoBehaviour
{
    private Animator animator;

    [HideInInspector]public bool targetHit;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        animator.SetFloat("TargetHit", targetHit ? 0.0f : 1.0f);
    }

    public void EndOfAnimation()
    {
        gameObject.SetActive(false);
    }
}
