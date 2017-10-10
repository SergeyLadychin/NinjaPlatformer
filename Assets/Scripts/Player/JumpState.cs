using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AbstractState
{
    public override StateType Type { get { return StateType.Jump; } }

    public JumpState(PlayerCharacterController characterController) : base(characterController) { }

    public override bool TryMakeTransition(StateInput input, out StateType newState)
    {
        newState = Type;

        if (input.grounded && input.jump && !isCurrent)
        {
            return true;
        }

        return false;
    }

    public override void Update(StateInput input)
    {
        controller.Jump();
    }
}
