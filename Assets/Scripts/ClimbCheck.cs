using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbCheck : MonoBehaviour
{
    private const float headCheckRadius = 0.1f;

    private Collider2D[] colliders;

    public float climbCheckRadius = 1.0f;
    public LayerMask whatIsClimbArea;
    public Transform headCheck;

    void Start()
    {
        colliders = new Collider2D[10];
    }

    public bool IsInClimbArea(out Vector3 climbPosition)
    {
        climbPosition = Vector3.zero;
        var collidersCount = Physics2D.OverlapCircleNonAlloc(transform.position, climbCheckRadius, colliders, whatIsClimbArea);
        for (int i = 0; i < collidersCount; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                climbPosition = colliders[i].gameObject.transform.Find("ClimbPosition").position;
                return true;
            }
        }

        return false;
    }

    public bool ClimbTopReached()
    {
        var collidersCount = Physics2D.OverlapCircleNonAlloc(headCheck.position, headCheckRadius, colliders);
        for (int i = 0; i < collidersCount; i++)
        {
            if (colliders[i].gameObject.name == "LadderTop")
            {
                return true;
            }
        }

        return false;
    }
}
