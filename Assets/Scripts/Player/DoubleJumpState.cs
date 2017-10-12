using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpState : AbstractState
{
    private bool canDoubleJump = true;

    public override StateType Type { get { return StateType.DoubleJump; } }

    public DoubleJumpState(PlayerCharacterController characterController, IStateInputProvider stateInputProvider) 
        : base(characterController, stateInputProvider) { }

    public override bool TryMakeTransition(StateInput input, out StateType newState)
    {
        newState = Type;

        if (input.grounded)
        {
            canDoubleJump = true;
        }

        //First check if player is in the air
        if (!input.grounded && Mathf.Abs(controller.GetVelocity().y) > 0.0f && !isCurrent)
        {
            //then check if player can jump
            if (input.jump && canDoubleJump)
            {
                canDoubleJump = false;
                return true;
            }
        }

        return false;
    }

    public override void Update()
    {
        controller.Jump();
    }
}
