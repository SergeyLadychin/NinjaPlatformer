using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : AbstractState
{
    public override StateType Type { get { return StateType.Run; } }

    public RunState(PlayerCharacterController characterController) : base(characterController) { }

    public override bool TryMakeTransition(StateInput input, out StateType newState)
    {
        newState = Type;

        if (Mathf.Abs(input.horizontal) < Constants.axisThreshold)
        {
            newState = StateType.Idle;
            return true;
        }

        return false;
    }
     
    public override void Update(StateInput input)
    {
        controller.Move(input.horizontal);
    }
}
