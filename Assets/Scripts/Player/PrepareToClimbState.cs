using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareToClimbState : IState
{
    private PlayerCharacterController controller;

    public StateType Type { get { return StateType.PrepareToClimb; } }

    public PrepareToClimbState(PlayerCharacterController characterController)
    {
        controller = characterController;
    }

    public void Enter()
    {

    }

    public bool TryMakeTransition(StateInput input, out StateType newState)
    {
        //Transitions like PrepareToClimb->Idle->PrepareToClimb->Idle->Climb won't be generated,
        //because Climb state first in hierarchy, so on each FixedUpdate transition to PrepareToClimb will be performed
        //until climb position will be reached.
        //If preparation to climbing will be canceled(i.e. button up released), character will stop moving.
        newState = StateType.Idle;
        return true;
    }

    public void Update()
    {
        //new Vector2(maxSpeed * Mathf.Sign((climbPosition - transform.position).x) * Mathf.Abs(climb)
        controller.Move(Input.GetAxis("Vertical"));
    }

    public void Exit()
    {

    }
}
