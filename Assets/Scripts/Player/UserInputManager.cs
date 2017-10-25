using System.Collections;
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
    }

    public void FixedUpdateInput()
    {
        stateInput.horizontal = Input.GetAxis("Horizontal");
        stateInput.vertical = Input.GetAxis("Vertical");
        stateInput.grounded = groundCheck.IsGrounded();

        stateInput.inClimbArea = climbCheck.IsInClimbArea(out stateInput.climbPosition);
        if (stateInput.inClimbArea)
        {
            stateInput.climbTopReached = climbCheck.ClimbTopReached();
        }
    }

    public bool GetButtonStatus(string button)
    {
        return Input.GetButtonDown(button);
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
