using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFallState : AbstractState
{
    public override StateType Type { get { return StateType.FreeFall; } }

    public FreeFallState(CharacterController2D characterController, IInputManager inputManager)
        : base(characterController, inputManager) { }

    public override bool TryMakeTransition(StateType current, out StateType newState)
    {
        newState = Type;
        var input = inputManager.GetStateInput();

        if (Mathf.Abs(controller.GetVelocity().y) > 0.0f || !input.grounded)
        {
            return true;
        }

        //grounded and current
        if (isCurrent)
        {
            newState = input.horizontal.buttonPressed ? StateType.Run : StateType.Idle;
            return true;
        }

        return false;
    }

    public override void Update()
    {
        controller.AirControl(inputManager.GetStateInput().horizontal.magnitude);
    }
}
