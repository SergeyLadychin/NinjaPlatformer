﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private bool facingRight = true;
    private float savedGravityScale;

    public float maxSpeed = 7.0f;
    public float maxClimbSpeed = 5.0f;
    public float jumpForce = 100.0f;
    public float doubleJumpForce = 100.0f;
    public bool airControl = true;
    [Range(0.0f, 1.0f)]
    public float airControlDegree = 1.0f;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float move)
    {
        MoveInternal(new Vector2(move * maxSpeed, _rigidbody2D.velocity.y), move);
    }

    public void Move(float moveX, float moveY)
    {
        MoveInternal(new Vector2(moveX * maxSpeed, moveY * maxSpeed), moveX);
    }

    public void AirControl(float move)
    {
        var velocity = move * maxSpeed;

        if (airControl)
        {
            velocity = Mathf.Lerp(_rigidbody2D.velocity.x, velocity, airControlDegree);
        }

        MoveInternal(new Vector2(velocity, _rigidbody2D.velocity.y), move);
    }

    public void Climb(float climb)
    {
        _rigidbody2D.velocity = new Vector2(0.0f, climb * maxClimbSpeed);
    }

    public bool CheckClimbPosition(Vector3 climbPosition, float epsilon = 0.1f)
    {
        return Mathf.Abs(transform.position.x - climbPosition.x) < epsilon;
    }

    public void SetRigidbodyPosition(Vector3 position)
    {
        _rigidbody2D.position = position;
    }

    public void Jump(float force)
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0.0f);
        _rigidbody2D.AddForce(Vector2.up * force);
    }

    public void TurnOffGravity()
    {
        savedGravityScale = _rigidbody2D.gravityScale;
        _rigidbody2D.gravityScale = 0.0f;
    }

    public void TurnOnGravity()
    {
        _rigidbody2D.gravityScale = savedGravityScale;
    }

    public Vector2 GetVelocity()
    {
        return _rigidbody2D.velocity;
    }

    public Vector3 GetFacingDirection()
    {
        return facingRight ? Vector3.right : Vector3.left;
    }

    public void SetSimulated(bool simulated)
    {
        _rigidbody2D.simulated = false;
    }

    void MoveInternal(Vector2 velocity, float facing)
    {
        _rigidbody2D.velocity = velocity;

        if (facing > 0 && !facingRight)
        {
            Flip();
        }
        else if (facing < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        var v = transform.localScale;
        v.x *= -1;
        transform.localScale = v;
    }

    public void SetFacingDirection(bool right)
    {
        if (right && facingRight)
            return;

        Flip();
    }
}
