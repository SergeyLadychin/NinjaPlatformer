using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INavigationPointWeaponAction
{
    WeaponActions CheckWeaponFire(Transform objectPosition, Vector2 direction);
}
