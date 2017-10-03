using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    private Animator animator;
    private bool grounded;
    private bool canClimb;
    private float hSpeed;
    private float vSpeed;
    private bool jumping;
    private bool doubleJumping;
    private bool climbing;
    private int doubleJumpCount = 0;

    private Vector3 climbPosition;

    public PlayerSubStates subState { get; private set; }
    public PlayerStateEnum current { get { return GetCurrentState(); } }

    public PlayerState(Animator playerAnimator)
    {
        animator = playerAnimator;
    }

    public void UpdateGrounded(bool grounded)
    {
        if (grounded)
        {
            this.doubleJumpCount = 0;
        }
        this.grounded = grounded;
        animator.SetBool("Grounded", grounded);
    }

    public void UpdateCanClimb(bool canClimb, Vector3 climbPosition)
    {
        this.canClimb = canClimb;
        this.climbPosition = climbPosition;
    }

    public void UpdateClimbing(float v)
    {
        subState = PlayerSubStates.None;
        var prevClimbing = climbing;
        this.climbing = canClimb && ((grounded && v > 0.0f) || (!grounded && (Mathf.Abs(v) > 0.0f || climbing)));

        if (prevClimbing && !climbing)
        {
            subState = PlayerSubStates.StopClimbing;
        }
        else if (!prevClimbing && climbing)
        {
            subState = PlayerSubStates.StartClimbing;
        }
    }

    public void UpdateHSpeed(float hSpeed)
    {
        this.hSpeed = hSpeed;
        animator.SetFloat("Speed", hSpeed);
    }

    public void UpdateVSpeed(float vSpeed)
    {
        this.vSpeed = vSpeed;
        animator.SetFloat("vSpeed", vSpeed);
    }

    public void UpdateJumping(bool jumping)
    {
        this.jumping = grounded && jumping;
        UpdateDoubleJumping(jumping);
    }

    public Vector3 GetClimbPosition()
    {
        if (canClimb)
            return climbPosition;
        return Vector3.zero;
    }

    public bool CheckIf(PlayerStateEnum state)
    {
        switch (state)
        {
            case PlayerStateEnum.Grounded:
            {
                if (grounded)
                    return true;
                break;
            }
        }
        return false;
    }

    private void UpdateDoubleJumping(bool doubleJumping)
    {
        if (!grounded)
        {
            this.doubleJumping = doubleJumping;
            doubleJumpCount++;
        }
    }

    private PlayerStateEnum GetCurrentState()
    {
        if (jumping && grounded && animator.GetBool("Grounded"))
        {
            return PlayerStateEnum.Jumping;
        }

        if (grounded && !climbing)
        {
            return PlayerStateEnum.Grounded;
        }
        else if (!jumping && Mathf.Abs(vSpeed) > 0.0f && !climbing)
        {
            if (doubleJumping && doubleJumpCount < 2)
            {
                return PlayerStateEnum.DoubleJumping;
            }
            return PlayerStateEnum.Falling;
        }
        else if (climbing)
        {
            return PlayerStateEnum.Climbing;
        }
        return PlayerStateEnum.Undefined;
    }
}

public enum PlayerSubStates
{
    None,
    StartClimbing,
    StopClimbing
}

public enum PlayerStateEnum
{
    Undefined = -1,
    Grounded,
    Jumping,
    DoubleJumping,
    Falling,
    Climbing
}
