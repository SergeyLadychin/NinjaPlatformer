using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbState : AbstractState
{
    public override StateType Type { get { return StateType.Climb; } }

    public ClimbState(PlayerCharacterController characterController) : base(characterController) { }

    public override void Enter()
    {
        base.Enter();
        controller.TurnOffGravity();
    }

    public override bool TryMakeTransition(StateInput input, out StateType newState)
    {
        newState = Type;
        if (isCurrent)
        {
            if (input.grounded && input.vertical < -Constants.axisThreshold)
            {
                newState = StateType.Idle;
                return true;
            }
            if (input.jump || Mathf.Abs(input.horizontal) > Constants.axisThreshold)
            {
                newState = StateType.FreeFall;
                return true;
            }
            return true;
        }

        if (input.inClimbArea)
        {
            //First check if pressed button up or down
            if (Mathf.Abs(input.vertical) > Constants.axisThreshold)
            {
                //then check if grounded
                if (input.grounded)
                {
                    //if grounded, then player can climb if button up was pressed
                    if (input.vertical > Constants.axisThreshold)
                    {
                        //if player near position where he/she can climb, then climb
                        //if not move player closer to this position.
                        if (controller.CheckClimbPosition(input.climbPosition))
                        {
                            return true;
                        }
                        newState = StateType.PrepareToClimb;
                        return true;
                    }
                }
                //if player in air near climb position, then climb
                else if (controller.CheckClimbPosition(input.climbPosition))
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    public override void Update(StateInput input)
    {
        if (input.climbTopReached && input.vertical > Constants.axisThreshold)
        {
            controller.Climb(0.0f);
        }
        else
        {
            controller.Climb(input.vertical);
        }
    }

    public override void Exit()
    {
        base.Exit();
        controller.TurnOnGravity();
    }
}
