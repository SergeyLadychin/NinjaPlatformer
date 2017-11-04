using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNextPointAction : MonoBehaviour, INavigationPointMoveAction
{
    private const float sqrDistanceThreshold = 0.01f;
    private bool actionCompleted;
    private bool changePoint;
    private NavigationPoint point;

    public MoveAxis moveDirection;
    public float validationDistance = sqrDistanceThreshold;
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
                if (moveDirection == MoveAxis.Horizontal)
                {
                    UpdateAxisInput(objectPosition, 1.0f, ref objectInput.horizontal);
                }
                else
                {
                    UpdateAxisInput(objectPosition, 1.0f, ref objectInput.vertical);
                }
                return;
            }

            if (waitBeforeChangePoint && moveDirection == MoveAxis.Horizontal)
            {
                if (turnToNextPointOnWait)
                {
                    UpdateAxisInput(objectPosition, -0.02f, ref objectInput.horizontal);
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

    private void UpdateAxisInput(Transform objectPosition, float moveMagnitude, ref AxisInput axisInput)
    {
        axisInput.magnitude = moveMagnitude * Mathf.Sign(GetVectorComponent(point.nextPoint.transform.position) - GetVectorComponent(objectPosition.position));
        axisInput.buttonPressed = true;
    }

    private float GetVectorComponent(Vector3 vector)
    {
        return moveDirection == MoveAxis.Horizontal ? vector.x : vector.y;
    }

    private bool NextPointReached(Transform objectPosition)
    {
        return (point.nextPoint.transform.position - objectPosition.position).sqrMagnitude <= validationDistance;
    }

    private IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(waitTime);
        changePoint = true;
    }
}
