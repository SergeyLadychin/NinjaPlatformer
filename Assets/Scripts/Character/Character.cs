using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected CharacterState characterState;
    protected CharacterController2D controller;
    protected Animator animator;

    public int health;

    void Awake()
    {
        characterState = GetComponent<CharacterState>();
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
    }

    public abstract void TakeDamage(int damageAmount, Vector2 hitPosition, Vector2 hitDirection);
}
