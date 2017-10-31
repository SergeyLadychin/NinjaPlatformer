using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInput
{
    public bool grounded;
    public bool inClimbArea;
    public bool climbTopReached;
    public float horizontal;
    public float vertical;
    public bool jump;
    public bool stopMoving;

    public Vector3 climbPosition;

    public void Clear()
    {
        grounded = false;
        inClimbArea = false;
        climbTopReached = false;
        horizontal = 0.0f;
        vertical = 0.0f;
        jump = false;
        stopMoving = false;
        climbPosition = Vector3.zero;
    }
}
