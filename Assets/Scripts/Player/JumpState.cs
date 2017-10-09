using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IState
{
    private PlayerCharacterController controller;

    public StateType Type { get { return StateType.Jump; } }

    public JumpState(PlayerCharacterController characterController)
    {
        controller = characterController;
    }

    public void Enter()
    {

    }

    public bool TryMakeTransition(StateInput input, out StateType newState)
    {
        if (input.grounded && input.jump)
        {
            newState = StateType.Jump;
            return true;
        }

        //always transit to fall state
        //do not stay in that state more then one frame
        newState = StateType.FreeFall;
        return true;
    }

    public void Update()
    {
        controller.Jump();
    }

    public void Exit()
    {

    }
}
