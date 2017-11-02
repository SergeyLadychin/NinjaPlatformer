using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AbstractState
{
    private StateType previousState;
    private WeaponManager weaponManager;

    public override StateType Type { get { return StateType.Attack; } }

    public AttackState(CharacterController2D characterController, IInputManager inputManager,
        WeaponManager weaponManager) : base(characterController, inputManager)
    {
        this.weaponManager = weaponManager;
    }

    public override bool TryMakeTransition(StateType current, out StateType newState)
    {
        newState = Type;

        if (isCurrent)
        {
            newState = previousState;
            return true;
        }

        if (current == StateType.Climb)
        {
            weaponManager.SetDefaultGroup();
        }

        if (weaponManager.CheckUserInput(current))
        {
            previousState = current;
            return true;
        }
        else
        {
            weaponManager.ClearUserInput();
        }

        return false;
    }

    public override void Update()
    {
        weaponManager.Fire();
    }
}
