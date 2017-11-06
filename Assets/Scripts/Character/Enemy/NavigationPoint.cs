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
        if (pointWeaponAction == null && pointMoveActions.Length == 0)
        {
            Debug.LogWarningFormat("Navigation point doesn't have any actions. Instance ID: {0}", this.gameObject.GetInstanceID());
        }
    }

    public bool GetActivateButtonStatus(string activationButton)
    {
        return pointWeaponAction != null && pointWeaponAction.SwitchWeaponGroup(activationButton);
    }

    public bool GetFireButtonStatus(Vector3 weaponPosition, Vector2 objectDirection, string button)
    {
        if (pointWeaponAction == null)
            return false;

        var actions = pointWeaponAction.CheckWeaponFire(weaponPosition, objectDirection);
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

        if (nextPoint == null)
            return false;

        bool result = false;
        if (pointMoveActions.Length != 0)
        {
            if (pointMoveActions.All(a => a.ChangePoint(objectPosition)))
            {
                result = pointWeaponAction == null || pointWeaponAction.ChangePoint();
            }
        }
        else
        {
            result = pointWeaponAction != null && pointWeaponAction.ChangePoint();
        }

        return result;
    }

    public void ResetActions()
    {
        if (pointWeaponAction != null)
            pointWeaponAction.Reset();

        for (int i = 0; i < pointMoveActions.Length; i++)
        {
            pointMoveActions[i].Reset();
        }
    }

    public Vector3 GetMousePosition(Vector3 weaponPosition)
    {
        if (pointWeaponAction == null)
            return Vector3.zero;

        return pointWeaponAction.GetMousePosition(weaponPosition);
    }
}

[Flags]
public enum WeaponActions
{
    None = 0,
    Holt = 1,
    Shoot = 2
}
