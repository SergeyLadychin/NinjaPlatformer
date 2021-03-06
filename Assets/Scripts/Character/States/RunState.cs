﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : AbstractState
{
    public override StateType Type { get { return StateType.Run; } }

    public RunState(CharacterController2D characterController, IInputManager inputManager)
        : base(characterController, inputManager) { }

    public override bool TryMakeTransition(StateType current, out StateType newState)
    {
        newState = Type;
        var input = inputManager.GetStateInput();

        if (!input.horizontal.buttonPressed)
        {
            newState = StateType.Idle;
            return true;
        }

        return false;
    }
     
    public override void Update()
    {
        controller.Move(inputManager.GetStateInput().horizontal.magnitude);
    }
}
