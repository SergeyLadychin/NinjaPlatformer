using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbJumpOffState : AbstractState
{
    public override StateType Type { get { return StateType.ClimbJumpOff; } }

    public ClimbJumpOffState(CharacterController2D characterController, IStateInputProvider stateInputProvider) 
        : base(characterController, stateInputProvider) { }

    public override bool TryMakeTransition(StateType current, out StateType newState)
    {
        newState = Type;

        if (isCurrent)
        {
            newState = StateType.FreeFall;
            return true;
        }

        return false;
    }

    public override void Update()
    {
        var input = inputProvider.Get();
        if (Mathf.Abs(input.horizontal) > Constants.axisThreshold)
        {
            controller.Move(1.0f * Mathf.Sign(input.horizontal));
        }
    }
}
