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

    public WeaponActions CheckWeaponFire(Transform objectPosition, Vector2 direction)
    {
        var result = WeaponActions.None;
        var hit = Physics2D.Raycast(transform.position, direction, viewDistance, solidObjects);
        Debug.DrawLine(transform.position, transform.position + (Vector3)direction * viewDistance, Color.green);
        if (hit.rigidbody && hit.rigidbody.CompareTag(Constants.PlayerTag))
        {
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

    private IEnumerator InteruptMovement()
    {
        stopMoving = true;
        yield return new WaitForSeconds(moveDelayOnShoot);
        stopMoving = false;
    }
}
