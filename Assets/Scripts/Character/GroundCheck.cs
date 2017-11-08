using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private const float groundCheckRadius = 0.1f;
    private Collider2D[] colliders;

    public float checkPointsDistance;
    public LayerMask whatIsGround;

    void Start()
    {
        colliders = new Collider2D[10];
    }

    public bool IsGrounded()
    {
        return IsLegGrounded(new Vector3(transform.position.x - checkPointsDistance, transform.position.y, transform.position.z))
               || IsLegGrounded(new Vector3(transform.position.x + checkPointsDistance, transform.position.y, transform.position.z));
    }

    private bool IsLegGrounded(Vector3 legPosition)
    {
        var collidersCount = Physics2D.OverlapCircleNonAlloc(legPosition, groundCheckRadius, colliders, whatIsGround);
        Debug.DrawLine(legPosition, legPosition + Vector3.down * groundCheckRadius, Color.green);
        for (int i = 0; i < collidersCount; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }

        return false;
    }
}
