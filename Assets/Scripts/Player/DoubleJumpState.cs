using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpState : AbstractState
{
    private bool canDoubleJump = true;

    public override StateType Type { get { return StateType.DoubleJump; } }

    public DoubleJumpState(PlayerCharacterController characterController) : base(characterController) { }

    public override bool TryMakeTransition(StateInput input, out StateType newState)
    {
        newState = Type;

        if (input.grounded)
        {
            canDoubleJump = true;
        }

        if (!input.grounded && Mathf.Abs(controller.GetVelocity().y) > 0.0f && !isCurrent)
        {
            if (input.jump && canDoubleJump)
            {
                canDoubleJump = false;
                return true;
            }
        }

        return false;
    }

    public override void Update(StateInput input)
    {
        controller.Jump();
    }
}
