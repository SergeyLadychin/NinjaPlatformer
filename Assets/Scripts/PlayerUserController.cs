using System.Collections;
using System.Collections.Generic;
using GravityAdventure;
using UnityEngine;

public class PlayerUserController : MonoBehaviour
{
    private PlayerCharacterController controller;
    private bool jump;
    private bool climbing;

    void Awake ()
	{
	    controller = GetComponent<PlayerCharacterController>();
	}

	void Update ()
    {
        controller.state.UpdateJumping(Input.GetButtonDown("Jump"));
    }

    void FixedUpdate()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        controller.state.UpdateHSpeed(Mathf.Abs(h));
        controller.state.UpdateClimbing(v);

        //controller.Move(MoveHelper.ReverseClamp01(h), MoveHelper.ReverseClamp01(v));
        controller.Move(h, v);

        controller.state.UpdateJumping(false);
    }
}
