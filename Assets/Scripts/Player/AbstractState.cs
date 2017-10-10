using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractState : IState
{
    protected PlayerCharacterController controller;
    protected bool isCurrent;

    public abstract StateType Type { get; }

    public AbstractState(PlayerCharacterController characterController)
    {
        controller = characterController;
    }

    public virtual void Enter()
    {
        isCurrent = true;
    }

    public abstract bool TryMakeTransition(StateInput input, out StateType newState);

    public abstract void Update(StateInput input);

    public virtual void Exit()
    {
        isCurrent = false;
    }
}
