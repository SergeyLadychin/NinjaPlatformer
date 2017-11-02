using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantShootWeaponAction : MonoBehaviour, INavigationPointWeaponAction
{
    public WeaponActions CheckWeaponFire(Transform objectPosition, Vector2 direction)
    {
        return WeaponActions.Shoot;
    }
}
