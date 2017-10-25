using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected Animator animator;
    protected CharacterController2D controller;

    public float cooldown;

    [HideInInspector]
    public IInputManager inputManager;

    public void Init(CharacterController2D characterController, Animator charcterAnimator, IInputManager inputManager)
    {
        controller = characterController;
        animator = charcterAnimator;
        this.inputManager = inputManager;
    }

    public virtual void Activate()
    {
        
    }

    public virtual void Deactivate()
    {

    }

    public abstract float Fire();
}
