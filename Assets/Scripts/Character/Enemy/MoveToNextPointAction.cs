using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNextPointAction : MonoBehaviour, INavigationPointMoveAction
{
    private const float sqrDistanceThreshold = 0.01f;
    private bool actionCompleted;
    private bool changePoint;
    private NavigationPoint point;

    public bool waitBeforeChangePoint;
    public float waitTime;
    public bool turnToNextPointOnWait;

    void Start()
    {
        point = GetComponent<NavigationPoint>();
    }

    public void ApplyToObject(Transform objectPosition, ref StateInput objectInput)
    {
        if (!actionCompleted)
        {
            if (!NextPointReached(objectPosition))
            {
                objectInput.horizontal = 1.0f * Mathf.Sign(point.nextPoint.transform.position.x - objectPosition.position.x);
                objectInput.horizontalButtonPressed = true;
                return;
            }

            if (waitBeforeChangePoint)
            {
                if (turnToNextPointOnWait)
                {
                    objectInput.horizontal = 0.02f * Mathf.Sign(point.transform.position.x - objectPosition.position.x);
                    objectInput.horizontalButtonPressed = true;
                }
                StartCoroutine(WaitToMove());
            }
            else
            {
                changePoint = true;
            }
            actionCompleted = true;
        }
    }

    public bool ChangePoint(Transform objectPosition)
    {
        return actionCompleted && changePoint;
    }

    public void Reset()
    {
        actionCompleted = false;
        changePoint = false;
    }

    private bool NextPointReached(Transform objectPosition)
    {
        return (point.nextPoint.transform.position - objectPosition.position).sqrMagnitude <= sqrDistanceThreshold;
    }

    private IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(waitTime);
        changePoint = true;
    }
}
