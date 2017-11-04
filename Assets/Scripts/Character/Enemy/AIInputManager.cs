using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInputManager : MonoBehaviour, IInputManager
{
    private StateInput stateInput;
    private NavigationPoint currentPoint;
    private CharacterController2D controller;

    public FacingDirection initialFacingDirection;
    public GroundCheck groundCheck;
    public ClimbCheck climbCheck;
    public NavigationPoint firstNavigationPoint;

    public bool GetButtonStatus(string button)
    {
        return currentPoint.GetAttackButtonStatus(transform, controller.GetFacingDirection(), button);
    }

    public void FixedUpdateInput()
    {
        stateInput.grounded = groundCheck.IsGrounded();
        if (climbCheck != null)
        {
            stateInput.inClimbArea = climbCheck.IsInClimbArea(out stateInput.climbPosition);
            if (stateInput.inClimbArea)
            {
                stateInput.climbTopReached = climbCheck.ClimbTopReached();
            }
        }
    }

    public StateInput GetStateInput()
    {
        return stateInput;
    }

    public void ClearInput()
    {
        stateInput.Clear();
    }

    void Awake()
    {
        stateInput = new StateInput();

        controller = GetComponent<CharacterController2D>();
        currentPoint = firstNavigationPoint;

        controller.SetFacingDirection(initialFacingDirection != FacingDirection.Left);
    }

    void Update()
    {
        if (currentPoint.UpdateStateInput(transform, ref stateInput))
        {
            currentPoint.ResetActions();
            currentPoint = currentPoint.nextPoint;
        }
    }
}

public enum FacingDirection
{
    Left,
    Right
}
