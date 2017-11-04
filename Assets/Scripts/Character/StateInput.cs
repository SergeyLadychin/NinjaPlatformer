using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StateInput
{
    public bool grounded;
    public bool inClimbArea;
    public bool climbTopReached;
    public AxisInput horizontal;
    public AxisInput vertical;
    public bool jump;

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
        horizontal.Clear();
        vertical.Clear();
        jump = false;
        climbPosition = Vector3.zero;
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
    }
}

public struct AxisInput
{
    public float magnitude;

    public bool buttonPressed;

    public void Clear()
    {
        magnitude = 0.0f;

        buttonPressed = false;
    }
}
