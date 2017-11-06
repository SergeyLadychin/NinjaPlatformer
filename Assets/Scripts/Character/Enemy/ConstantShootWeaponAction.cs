using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantShootWeaponAction : MonoBehaviour, INavigationPointWeaponAction
{
    public bool SwitchWeaponGroup(string activationButton)
    {
        return false;
    }

    public WeaponActions CheckWeaponFire(Vector3 objectPosition, Vector2 direction)
    {
        return WeaponActions.Shoot;
    }

    public Vector3 GetMousePosition(Vector3 weaponPosition)
    {
        throw new System.NotImplementedException();
    }

    public bool ChangePoint()
    {
        return true;
    }

    public void Reset()
    {
        //Do nothing
    }
}
