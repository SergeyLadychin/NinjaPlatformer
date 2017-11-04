using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareToClimbState : AbstractState
{
    public override StateType Type { get { return StateType.PrepareToClimb; } }

    public PrepareToClimbState(CharacterController2D characterController, IInputManager inputManager)
        : base(characterController, inputManager) { }

    public override bool TryMakeTransition(StateType current, out StateType newState)
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
        var input = inputManager.GetStateInput();
        controller.Move(Mathf.Sign((input.climbPosition - controller.transform.position).x) * input.vertical.magnitude);
    }
}
