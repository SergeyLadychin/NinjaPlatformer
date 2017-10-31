using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private const float groundCheckRadius = 0.1f;
    private Collider2D[] colliders;

    public LayerMask whatIsGround;

    void Start()
    {
        colliders = new Collider2D[10];
    }

    public bool IsGrounded()
    {
        var collidersCount = Physics2D.OverlapCircleNonAlloc(transform.position, groundCheckRadius, colliders, whatIsGround);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * groundCheckRadius, Color.green);
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
