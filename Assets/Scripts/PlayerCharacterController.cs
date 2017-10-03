using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GravityAdventure
{
    public class PlayerCharacterController : MonoBehaviour
    {
        public float maxSpeed = 7f;
        public float jumpForce = 100f;
        public bool airControl = true;
        [Range(0f, 1f)]
        public float airControlDegree = 1f;
        public LayerMask whatIsGround;

        public PlayerState state;

        public bool canClimb
        {
            get { return canClimbInternal; }
            set
            {
                if (climbing)
                    _rigidbody2D.gravityScale = 1.0f;

                climbing = false;
                canClimbInternal = value;
            }
        }

        private Rigidbody2D _rigidbody2D;
        private bool facingRight = true;
        private const float groundCheckRadius = 0.1f;
        private Transform groundCheck;

        private bool canClimbInternal;
        private bool climbing;

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            groundCheck = transform.Find("GroundCheck");
            state = new PlayerState(GetComponent<Animator>());
        }

        void FixedUpdate()
        {
            bool isGrounded = false;

            var colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    isGrounded = true;
            }

            state.UpdateGrounded(isGrounded);
            state.UpdateVSpeed(_rigidbody2D.velocity.y);
        }

        public void Move(float move, float climb)
        {
            UpdateStateChange();
            var currentState = state.current;
            switch (currentState)
            {
                case PlayerStateEnum.Grounded:
                {
                    MoveInternal(new Vector2(move * maxSpeed, _rigidbody2D.velocity.y));
                    break;
                }
                case PlayerStateEnum.Falling:
                {
                    var velocity = move * maxSpeed;
                    
                    if (airControl)
                    {
                        velocity = Mathf.Lerp(_rigidbody2D.velocity.x, velocity, airControlDegree);
                    }

                    MoveInternal(new Vector2(velocity, _rigidbody2D.velocity.y));
                    break;
                }
                case PlayerStateEnum.Climbing:
                {
                    var climbPosition = state.GetClimbPosition();
                    
                    if (Mathf.Abs(transform.position.x - climbPosition.x) < 0.1f)
                    {
                        _rigidbody2D.velocity = new Vector2(0.0f, climb * maxSpeed);
                    }
                    else if (state.CheckIf(PlayerStateEnum.Grounded))
                    {
                        state.UpdateHSpeed(maxSpeed);
                        MoveInternal(new Vector2(maxSpeed * Mathf.Sign((climbPosition - transform.position).x), _rigidbody2D.velocity.y));
                    }
                    break;
                }
                case PlayerStateEnum.Jumping:
                case PlayerStateEnum.DoubleJumping:
                {
                    state.UpdateGrounded(false);
                    _rigidbody2D.AddForce(new Vector2(0f, jumpForce));
                    break;
                }
                case PlayerStateEnum.Undefined:
                {
                    Debug.LogError("Undefined player state.");
                    break;
                }
            }
        }

        private void UpdateStateChange()
        {
            switch (state.subState)
            {
                case PlayerSubStates.StartClimbing:
                {
                    _rigidbody2D.gravityScale = 0.0f;
                    break;
                }
                case PlayerSubStates.StopClimbing:
                {
                    _rigidbody2D.gravityScale = 1.0f;
                    break;
                }
            }
        }

        void MoveInternal(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity;

            if (velocity.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (velocity.x < 0 && facingRight)
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
}
