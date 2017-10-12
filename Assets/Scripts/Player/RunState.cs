using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : AbstractState
{
    public override StateType Type { get { return StateType.Run; } }

    public RunState(PlayerCharacterController characterController, IStateInputProvider stateInputProvider)
        : base(characterController, stateInputProvider) { }

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
     
    public override void Update()
    {
        controller.Move(inputProvider.Get().horizontal);
    }
}
