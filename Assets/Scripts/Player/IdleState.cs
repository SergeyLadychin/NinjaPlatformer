using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private PlayerCharacterController controller;

    public StateType Type { get { return StateType.Idle; } }

    public IdleState(PlayerCharacterController characterController)
    {
        controller = characterController;
    }

    public void Enter()
    {
        
    }

    public bool TryMakeTransition(StateInput input, out StateType newState)
    {
        newState = Type;
        if (Mathf.Abs(input.horizontal) > Constants.axisThreshold)
        {
            newState = StateType.Run;
            return true;
        }
        return false;
    }

    public void Update()
    {
        
    }

    public void Exit()
    {

    }
}
