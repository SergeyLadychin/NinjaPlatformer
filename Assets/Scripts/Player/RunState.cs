using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    private PlayerCharacterController controller;

    public StateType Type { get { return StateType.Run; } }

    public RunState(PlayerCharacterController characterController)
    {
        controller = characterController;
    }

    public void Enter()
    {

    }

    public bool TryMakeTransition(StateInput input, out StateType newState)
    {
        newState = Type;

        if (Mathf.Abs(input.horizontal) < Constants.axisThreshold)
        {
            newState = StateType.Idle;
            return true;
        }

        return false;
    }
     
    public void Update()
    {
        var h = Input.GetAxis("Horizontal");

        controller.Move(h);
    }

    public void Exit()
    {

    }
}
