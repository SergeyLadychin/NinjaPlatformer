using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbState : AbstractState
{
    private Animator animator;
    private float speedFraction;
    private float currentSpeed;

    public override StateType Type { get { return StateType.Climb; } }

    public ClimbState(CharacterController2D characterController, IInputManager inputManager, Animator characterAnimator)
        : base(characterController, inputManager)
    {
        animator = characterAnimator;
    }

    public override void Enter()
    {
        base.Enter();
        controller.SetRigidbodyPosition(new Vector3(inputManager.GetStateInput().climbPosition.x, controller.transform.position.y, controller.transform.position.z));
        controller.TurnOffGravity();

        currentSpeed = 0.0f;
        speedFraction = controller.maxClimbSpeed / 10.0f; //10 is count of animation frames
        animator.SetBool("Climb", true);
    }

    public override bool TryMakeTransition(StateType current, out StateType newState)
    {
        newState = Type;
        var input = inputManager.GetStateInput();
        if (isCurrent)
        {
            if (input.grounded && input.vertical.magnitude < -Constants.axisThreshold)
            {
                newState = StateType.Idle;
                return true;
            }
            if (input.jump || input.horizontal.buttonPressed)
            {
                newState = StateType.ClimbJumpOff;
                return true;
            }
            return true;
        }

        if (input.inClimbArea)
        {
            //First check if pressed button up or down
            if (input.vertical.buttonPressed && !input.horizontal.buttonPressed)
            {
                //then check if grounded
                if (input.grounded)
                {
                    //if grounded, then player can climb if button up was pressed
                    if (input.vertical.magnitude > Constants.axisThreshold)
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
                else if (controller.CheckClimbPosition(input.climbPosition, 0.3f))
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    public override void Update()
    {
        var input = inputManager.GetStateInput();
        if (input.climbTopReached && input.vertical.magnitude > Constants.axisThreshold)
        {
            controller.Climb(0.0f);
        }
        else
        {
            if (Mathf.Abs(input.vertical.magnitude) > Constants.axisThreshold)
            {
                currentSpeed = (currentSpeed + speedFraction) % controller.maxClimbSpeed;
            }
            controller.Climb(input.vertical.magnitude);
        }
        animator.SetFloat("ClimbSpeed", currentSpeed);
    }

    public override void Exit()
    {
        base.Exit();
        controller.TurnOnGravity();

        animator.SetBool("Climb", false);
    }
}
