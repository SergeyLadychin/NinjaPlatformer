using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpState : AbstractState
{
    private bool canDoubleJump = true;

    public override StateType Type { get { return StateType.DoubleJump; } }

    public DoubleJumpState(CharacterController2D characterController, IStateInputProvider stateInputProvider) 
        : base(characterController, stateInputProvider) { }

    public override bool TryMakeTransition(StateType current, out StateType newState)
    {
        newState = Type;
        var input = inputProvider.Get();

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
