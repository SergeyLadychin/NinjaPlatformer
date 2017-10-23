using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractState : IState
{
    protected CharacterController2D controller;
    protected IStateInputProvider inputProvider;
    protected bool isCurrent;

    public abstract StateType Type { get; }

    public AbstractState(CharacterController2D characterController, IStateInputProvider stateInputProvider)
    {
        controller = characterController;
        inputProvider = stateInputProvider;
    }

    public virtual void Enter()
    {
        isCurrent = true;
    }

    public abstract bool TryMakeTransition(StateType current, out StateType newState);

    public abstract void Update();

    public virtual void Exit()
    {
        isCurrent = false;
    }
}
