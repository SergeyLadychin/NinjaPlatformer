﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputManager : MonoBehaviour, IInputManager
{
    private StateInput stateInput;

    public GroundCheck groundCheck;
    public ClimbCheck climbCheck;

	void Awake ()
    {
		stateInput = new StateInput();
	}

	void Update ()
    {
        //Check if flag was dropped instead of just setting field 'jump' every Update call,
        //because Update can be called several times before FixedUpdate will be called.
        if (!stateInput.jump)
        {
            stateInput.jump = Input.GetButtonDown("Jump");
        }
        if (!stateInput.horizontal.buttonPressed)
        {
            stateInput.horizontal.buttonPressed = Input.GetButton("Horizontal");
        }
        if (!stateInput.vertical.buttonPressed)
        {
            stateInput.vertical.buttonPressed = Input.GetButton("Vertical");
        }
    }

    public void FixedUpdateInput()
    {
        stateInput.horizontal.magnitude = Input.GetAxis("Horizontal");
        stateInput.vertical.magnitude = Input.GetAxis("Vertical");
        stateInput.grounded = groundCheck.IsGrounded();

        stateInput.inClimbArea = climbCheck.IsInClimbArea(out stateInput.climbPosition);
        if (stateInput.inClimbArea)
        {
            stateInput.climbTopReached = climbCheck.ClimbTopReached();
        }
    }

    public bool GetFireButtonStatus(Vector3 weaponPosition, string button)
    {
        return this.enabled && Input.GetButtonDown(button);
    }

    public bool GetActivateButtonStatus(string button)
    {
        return this.enabled && Input.GetButtonDown(button);
    }

    public Vector3 GetMousePosition(Vector3 weaponPosition)
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public StateInput GetStateInput()
    {
        return stateInput;
    }

    public void ClearInput()
    {
        stateInput.Clear();
    }
}
