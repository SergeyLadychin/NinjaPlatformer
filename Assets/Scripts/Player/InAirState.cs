using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : IState
{
    private PlayerCharacterController controller;

    public StateType Type { get { return StateType.FreeFall; } }

    public InAirState(PlayerCharacterController characterController)
    {
        controller = characterController;
    }

    public void Enter()
    {

    }

    public bool TryMakeTransition(StateInput input, out StateType newState)
    {
        newState = Type;

        if (input.grounded)
        {
            newState = StateType.Idle;
            return true;
        }

        return false;
    }

    public void Update()
    {
        var h = Input.GetAxis("Horizontal");

        controller.AirControl(h);
    }

    public void Exit()
    {

    }
}
