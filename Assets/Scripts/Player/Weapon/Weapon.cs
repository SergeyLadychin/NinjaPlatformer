using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float cooldown;
    protected Animator animator;
    protected CharacterController2D controller;

    public void Init(CharacterController2D characterController, Animator charcterAnimator)
    {
        controller = characterController;
        animator = charcterAnimator;
    }

    public virtual void Activate()
    {
        
    }

    public virtual void Deactivate()
    {

    }

    public abstract float Fire();
}
