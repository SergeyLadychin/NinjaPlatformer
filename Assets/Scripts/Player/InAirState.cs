using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : AbstractState
{
    public override StateType Type { get { return StateType.FreeFall; } }

    public InAirState(PlayerCharacterController characterController) : base(characterController) { }

    public override bool TryMakeTransition(StateInput input, out StateType newState)
    {
        newState = Type;

        if (Mathf.Abs(controller.GetVelocity().y) > 0.0f)
        {
            return true;
        }

        if (input.grounded && isCurrent)
        {
            newState = StateType.Idle;
            return true;
        }

        return false;
    }

    public override void Update(StateInput input)
    {
        controller.AirControl(input.horizontal);
    }
}
