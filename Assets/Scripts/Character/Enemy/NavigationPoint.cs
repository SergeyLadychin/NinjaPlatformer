using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NavigationPoint : MonoBehaviour
{
    private INavigationPointMoveAction[] pointMoveActions;
    private INavigationPointWeaponAction pointWeaponAction;

    private bool postponeMoveActions;

    public NavigationPoint nextPoint;

    void Awake()
    {
        pointMoveActions = GetComponents<INavigationPointMoveAction>();
        pointWeaponAction = GetComponent<INavigationPointWeaponAction>();
    }

    public bool GetAttackButtonStatus(Transform objectPosition, Vector2 objectDirection, string button)
    {
        var actions = pointWeaponAction.CheckWeaponFire(objectPosition, objectDirection);
        postponeMoveActions = (actions & WeaponActions.Holt) == WeaponActions.Holt;
        return (actions & WeaponActions.Shoot) == WeaponActions.Shoot;
    }

    public bool UpdateStateInput(Transform objectPosition, ref StateInput objectInput)
    {
        if (postponeMoveActions)
            return false;

        for (int i = 0; i < pointMoveActions.Length; i++)
        {
            pointMoveActions[i].ApplyToObject(objectPosition, ref objectInput);
        }
        return pointMoveActions.Length != 0 && pointMoveActions.All(a => a.ChangePoint(objectPosition));
    }

    public void ResetActions()
    {
        for (int i = 0; i < pointMoveActions.Length; i++)
        {
            pointMoveActions[i].Reset();
        }
    }
}

[Flags]
public enum WeaponActions
{
    None = 0,
    Holt = 1,
    Shoot = 2
}
