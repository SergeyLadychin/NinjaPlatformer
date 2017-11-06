using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INavigationPointWeaponAction
{
    bool SwitchWeaponGroup(string activationButton);
    WeaponActions CheckWeaponFire(Vector3 objectPosition, Vector2 direction);
    Vector3 GetMousePosition(Vector3 weaponPosition);
    bool ChangePoint();
    void Reset();
}
