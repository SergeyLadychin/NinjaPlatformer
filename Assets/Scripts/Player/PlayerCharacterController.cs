using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    public float maxSpeed = 7f;
    public float jumpForce = 100f;
    public bool airControl = true;
    [Range(0f, 1f)]
    public float airControlDegree = 1f;
    
    private Rigidbody2D _rigidbody2D;
    private bool facingRight = true;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float move)
    {
        MoveInternal(new Vector2(move * maxSpeed, _rigidbody2D.velocity.y), move);
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
        _rigidbody2D.velocity = new Vector2(0.0f, climb * maxSpeed);
    }

    public bool CheckClimbPosition(Vector3 climbPosition)
    {
        return Mathf.Abs(transform.position.x - climbPosition.x) < 0.1f;
    }

    public void SetRigidbodyPositionX(float x)
    {
        var rPosition = _rigidbody2D.position;
        rPosition.x = x;
        _rigidbody2D.position = rPosition;
    }

    public void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0.0f);
        _rigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    public void TurnOffGravity()
    {
        _rigidbody2D.gravityScale = 0.0f;
    }

    public void TurnOnGravity()
    {
        _rigidbody2D.gravityScale = 1.0f;
    }

    public Vector2 GetVelocity()
    {
        return _rigidbody2D.velocity;
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
}
