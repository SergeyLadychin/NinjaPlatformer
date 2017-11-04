using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AbstractState
{
    public override StateType Type { get { return StateType.Jump; } }

    public JumpState(CharacterController2D characterController, IInputManager inputManager)
        : base(characterController, inputManager) { }

    public override bool TryMakeTransition(StateType current, out StateType newState)
    {
        newState = Type;
        var input = inputManager.GetStateInput();

        if (input.grounded && input.jump && !isCurrent)
        {
            return true;
        }

        return false;
    }

    public override void Update()
    {
        controller.Jump(controller.jumpForce);
    }
}
