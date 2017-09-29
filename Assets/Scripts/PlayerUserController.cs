using System.Collections;
using System.Collections.Generic;
using GravityAdventure;
using UnityEngine;

public class PlayerUserController : MonoBehaviour
{
    private PlayerCharacterController controller;
    private bool jump;

    // Use this for initialization
    void Awake ()
	{
	    controller = GetComponent<PlayerCharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!jump)
        {
            jump = Input.GetButtonDown("Jump");
        }
    }

    void FixedUpdate()
    {
        var h = Input.GetAxis("Horizontal");

        controller.Move(h, jump);

        jump = false;
    }
}
