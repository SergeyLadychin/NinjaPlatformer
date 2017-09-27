using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GravityAdventure
{
    public class PlayerController : MonoBehaviour
    {
        public float maxSpeed = 7f;
        public float jumpForce = 100f;
        public bool airControl = true;
        public LayerMask whatIsGround;

        private Rigidbody2D _rigidbody2D;
        private bool facingRight = true;
        private const float groundCheckRadius = 0.2f;
        private Transform groundCheck;
        private bool isGrounded;
        private bool jump;
        private Animator anim;

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            groundCheck = transform.Find("GroundCheck");
            anim = GetComponent<Animator>();
        }

        void FixedUpdate()
        {
            isGrounded = false;

            var colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    isGrounded = true;
            }

            anim.SetBool("Ground", isGrounded);

            if (isGrounded || airControl)
            {
                var h = Input.GetAxis("Horizontal");
                _rigidbody2D.velocity = new Vector2(h * maxSpeed, _rigidbody2D.velocity.y);

                anim.SetFloat("Speed", Mathf.Abs(h));

                if (h > 0 && !facingRight)
                {
                    Flip();
                }
                else if (h < 0 && facingRight)
                {
                    Flip();
                }
            }

            if (jump && isGrounded)
            {
                isGrounded = false;
                anim.SetBool("Ground", false);
                _rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            }

            jump = false;
        }

        void Update()
        {
            if (!jump)
            {
                jump = Input.GetButtonDown("Jump");
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
