using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected CharacterState characterState;
    protected CharacterController2D controller;
    protected Animator animator;
    protected MonoBehaviour inputManager;

    public int health;

    void Awake()
    {
        characterState = GetComponent<CharacterState>();
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();

        inputManager = GetComponent<IInputManager>() as MonoBehaviour;
    }

    void OnEnable()
    {
        SubscribeEvents();
    }

    void OnDisable()
    {
        UnsubscribeEvents();
    }

    public abstract void TakeDamage(int damageAmount, Vector2 hitPosition, Vector2 hitDirection);

    private void Activate()
    {
        inputManager.enabled = true;
    }

    protected virtual void SubscribeEvents()
    {
        EventManager.StartListen(Constants.ActivateCharactersEvent, Activate);
    }

    protected virtual void UnsubscribeEvents()
    {
        EventManager.StopListen(Constants.ActivateCharactersEvent, Activate);
    }
}
