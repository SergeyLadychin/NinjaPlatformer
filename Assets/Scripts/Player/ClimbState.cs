using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbState : IState
{
    private PlayerCharacterController controller;

    public StateType Type { get { return StateType.Climb; } }

    public ClimbState(PlayerCharacterController characterController)
    {
        controller = characterController;
    }

    public void Enter()
    {
        controller.TurnOffGravity();
    }

    public bool TryMakeTransition(StateInput input, out StateType newState)
    {
        newState = Type;

        if (input.inClimbArea)
        {
            if (Mathf.Abs(input.vertical) > Constants.axisThreshold)
            {
                if (input.grounded)
                {
                    if (input.vertical > Constants.axisThreshold)
                    {
                        if (controller.CheckClimbPosition(input.climbPosition))
                        {
                            if (input.jump || Mathf.Abs(input.horizontal) > Constants.axisThreshold)
                            {
                                newState = StateType.FreeFall;
                                return true;
                            }
                            return true;
                        }
                        newState = StateType.PrepareToClimb;
                        return true;
                    }
                }
                else if (controller.CheckClimbPosition(input.climbPosition))
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    public void Update()
    {
        controller.Climb(Input.GetAxis("Vertical"));
    }

    public void Exit()
    {
        controller.TurnOnGravity();
    }
}
