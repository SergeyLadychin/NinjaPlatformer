using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBombWeaponAction : MonoBehaviour, INavigationPointWeaponAction
{
    private Vector3 lastMousePosition = Vector3.zero;
    
    private ThrowBombSteps currentStep;

    public string activateButton;
    public Transform player;
    public float timeBeforeThrow;
    public float viewDistance;
    public LayerMask solidObjects;
    public Transform defaultTarget;

    public bool SwitchWeaponGroup(string activationButton)
    {
        if (!CheckStep(ThrowBombSteps.BombActivated) && activationButton == activateButton)
        {
            SetStep(ThrowBombSteps.BombActivated);
            return true;
        }
        return false;
    }

    public WeaponActions CheckWeaponFire(Vector3 objectPosition, Vector2 direction)
    {
        if (!CheckStep(ThrowBombSteps.BombThrowed))
        {
            if (CheckStep(ThrowBombSteps.BeforeThrowTimerExpired))
            {
                SetStep(ThrowBombSteps.BombThrowed);
                return WeaponActions.Shoot;
            }
            else if (!CheckStep(ThrowBombSteps.BeforeThrowTimerStarted))
            {
                SetStep(ThrowBombSteps.BeforeThrowTimerStarted);
                StartCoroutine(Timer(ThrowBombSteps.BeforeThrowTimerExpired));
            }
        }
        else if (!CheckStep(ThrowBombSteps.AfterThrowTimerStarted))
        {
            SetStep(ThrowBombSteps.AfterThrowTimerStarted);
            StartCoroutine(Timer(ThrowBombSteps.AfterThrowTimerExpired));
        }
        return WeaponActions.None;
    }

    public Vector3 GetMousePosition(Vector3 weaponPosition)
    {
        var hit = Physics2D.Raycast(weaponPosition, player.position - weaponPosition, viewDistance, solidObjects);
        if (hit.rigidbody && hit.rigidbody.CompareTag(Constants.PlayerTag))
        {
            lastMousePosition = player.position;
        }
        else
        {
            lastMousePosition = defaultTarget.position;
        }
        return lastMousePosition;
    }

    public bool ChangePoint()
    {
        return CheckStep(ThrowBombSteps.AfterThrowTimerExpired);
    }

    public void Reset()
    {
        currentStep = ThrowBombSteps.None;
    }

    private bool CheckStep(ThrowBombSteps step)
    {
        return (currentStep & step) == step;
    }

    private void SetStep(ThrowBombSteps step)
    {
        currentStep |= step;
    }

    private IEnumerator Timer(ThrowBombSteps step)
    {
        yield return new WaitForSeconds(timeBeforeThrow);
        SetStep(step);
    }

    [Flags]
    private enum ThrowBombSteps
    {
        None = 0,
        BombActivated = 1,
        BeforeThrowTimerStarted = 2,
        BeforeThrowTimerExpired = 4,
        BombThrowed = 8,
        AfterThrowTimerStarted = 16,
        AfterThrowTimerExpired = 32
    }
}
