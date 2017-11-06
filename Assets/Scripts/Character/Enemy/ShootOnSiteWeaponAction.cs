using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootOnSiteWeaponAction : MonoBehaviour, INavigationPointWeaponAction
{
    private bool stopMoving;

    public bool stopToShoot;
    public float moveDelayOnShoot;
    public float viewDistance;
    public LayerMask solidObjects;

    public bool SwitchWeaponGroup(string activationButton)
    {
        return false;
    }

    public WeaponActions CheckWeaponFire(Vector3 weaponPosition, Vector2 direction)
    {
        var result = WeaponActions.None;
        var hit = Physics2D.Raycast(weaponPosition, direction, viewDistance, solidObjects);
        Debug.DrawLine(weaponPosition, weaponPosition + (Vector3)direction * viewDistance, Color.green);
        if (hit.rigidbody && hit.rigidbody.CompareTag(Constants.PlayerTag))
        {
            Debug.DrawLine(weaponPosition, weaponPosition + (Vector3)direction * viewDistance, Color.red);
            if (stopToShoot)
            {
                if (!stopMoving)
                {
                    StartCoroutine(InteruptMovement());
                }
            }
            result = result | WeaponActions.Shoot;
        }

        if (stopMoving && stopToShoot)
        {
            result = result | WeaponActions.Holt;
        }

        return result;
    }

    public Vector3 GetMousePosition(Vector3 weaponPosition)
    {
        return Vector3.zero;
    }

    public bool ChangePoint()
    {
        return true;
    }

    public void Reset()
    {
        //Do nothing
    }

    private IEnumerator InteruptMovement()
    {
        stopMoving = true;
        yield return new WaitForSeconds(moveDelayOnShoot);
        stopMoving = false;
    }
}
