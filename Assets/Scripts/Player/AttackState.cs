using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AbstractState
{
    private WeaponManager weaponManager;
    private float cooldown = -1.0f;

    public override StateType Type { get { return StateType.Attack; } }

    public AttackState(CharacterController2D characterController, IStateInputProvider inputProvider,
        WeaponManager weaponManager) : base(characterController, inputProvider)
    {
        this.weaponManager = weaponManager;
    }

    public override bool TryMakeTransition(StateType current, out StateType newState)
    {
        newState = Type;
        
        if (cooldown < 0.0f)
        {
            if ((current == StateType.Idle || current == StateType.Run || current == StateType.FreeFall) && weaponManager.CheckUserInput())
                return true;
        }
        else
        {
            cooldown -= Time.fixedDeltaTime;
        }
        
        return false;
    }

    public override void Update()
    {
        cooldown = weaponManager.Fire();
    }
}
