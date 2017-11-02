using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StateInput
{
    public bool grounded;
    public bool inClimbArea;
    public bool climbTopReached;
    public float horizontal;
    public float vertical;
    public bool jump;

    public bool horizontalButtonPressed;
    public bool verticalButtonPressed;

    public Vector3 climbPosition;

    public StateInput(StateInput input) : this()
    {
        Populate(input);
    }

    public void Clear()
    {
        grounded = false;
        inClimbArea = false;
        climbTopReached = false;
        horizontal = 0.0f;
        vertical = 0.0f;
        jump = false;
        climbPosition = Vector3.zero;

        horizontalButtonPressed = false;
        verticalButtonPressed = false;
    }

    public void Populate(StateInput input)
    {
        grounded = input.grounded;
        inClimbArea = input.inClimbArea;
        climbTopReached = input.climbTopReached;
        horizontal = input.horizontal;
        vertical = input.vertical;
        jump = input.jump;
        climbPosition = input.climbPosition;

        horizontalButtonPressed = input.horizontalButtonPressed;
        verticalButtonPressed = input.verticalButtonPressed;
    }
}
