using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareToClimbState : AbstractState
{
    public override StateType Type { get { return StateType.PrepareToClimb; } }

    public PrepareToClimbState(PlayerCharacterController characterController, IStateInputProvider stateInputProvider)
        : base(characterController, stateInputProvider) { }

    public override bool TryMakeTransition(StateInput input, out StateType newState)
    {
        //Transitions like PrepareToClimb->Idle->PrepareToClimb->Idle->Climb won't be generated,
        //because Climb state first in hierarchy, so on each FixedUpdate transition to PrepareToClimb will be performed
        //until climb position will be reached.
        //If preparation to climbing will be canceled(i.e. button up released), character will stop moving.
        newState = StateType.Idle;
        return true;
    }

    public override void Update()
    {
        var input = inputProvider.Get();
        controller.Move(Mathf.Sign((input.climbPosition - controller.transform.position).x) * input.vertical);
    }
}
