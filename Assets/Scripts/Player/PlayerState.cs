using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private StateInput stateInput;

    private const float groundCheckRadius = 0.1f;
    private const float axisThreshold = 0.001f;
    private const float velocityThreshold = 0.01f;

    private Transform groundCheck;
    private Transform climbCheck;
    private Animator animator;
    private Rigidbody2D _rigidbody2D;

    public bool allowDoubleJump;
    public LayerMask whatIsGround;
    public LayerMask whatIsLadder;
    public float climbCheckRadius = 5.0f;

    private IState currentState
    {
        get { return statesHierarchy[statesHierarchy.Count - 1]; }
        set { statesHierarchy[statesHierarchy.Count - 1] = value; }
    }

    private Dictionary<StateType, IState> allStates;
    private List<IState> statesHierarchy;

    void Awake()
    {
        stateInput = new StateInput();
        var characterController = GetComponent<PlayerCharacterController>();

        allStates = new Dictionary<StateType, IState>();
        allStates.Add(StateType.Idle, new IdleState(characterController));
        allStates.Add(StateType.Run, new RunState(characterController));
        allStates.Add(StateType.Jump, new JumpState(characterController));
        allStates.Add(StateType.FreeFall, new InAirState(characterController));
        allStates.Add(StateType.PrepareToClimb, new PrepareToClimbState(characterController));
        allStates.Add(StateType.Climb, new ClimbState(characterController));

        statesHierarchy = new List<IState>();
        statesHierarchy.Add(allStates[StateType.Climb]);
        statesHierarchy.Add(allStates[StateType.Jump]);
        statesHierarchy.Add(allStates[StateType.FreeFall]);
        statesHierarchy.Add(allStates[StateType.Idle]);

        animator = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheck");
        climbCheck = transform.Find("ClimbCheck");
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        stateInput.jump = Input.GetButtonDown("Jump");
        stateInput.horizontal = Input.GetAxis("Horizontal");
        stateInput.vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        bool isGrounded = false;

        var colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }

        stateInput.grounded = isGrounded;
        animator.SetBool("Grounded", isGrounded);

        bool inClimbArea = false;
        Vector3 whereCanClimb = Vector3.zero;
        colliders = Physics2D.OverlapCircleAll(climbCheck.position, climbCheckRadius, whatIsLadder);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                inClimbArea = true;
                whereCanClimb = colliders[i].gameObject.transform.Find("ClimbPosition").position;
            }
        }

        stateInput.inClimbArea = inClimbArea;
        stateInput.climbPosition = whereCanClimb;

        animator.SetFloat("hSpeed", Mathf.Abs(_rigidbody2D.velocity.x));
        animator.SetFloat("vSpeed", _rigidbody2D.velocity.y);

        UpdateState();
        stateInput.jump = false;
    }

    private void UpdateState()
    {
        for (int i = 0; i < statesHierarchy.Count; i++)
        {
            StateType nextState;
            if (statesHierarchy[i].TryMakeTransition(stateInput, out nextState))
            {
                IState state;
                if (allStates.TryGetValue(nextState, out state))
                {
                    currentState.Exit();
                    currentState = state;
                    currentState.Enter();
                    break;
                }
                else
                {
                    Debug.LogErrorFormat("State {0} was not found in AllStates collection.", nextState);
                }
            }
        }
        currentState.Update();
    }
}

public enum StateType
{
    TransitionFail = -2,
    Undefined = -1,
    Idle,
    Run,
    Jump,
    DoubleJumping,
    FreeFall,
    PrepareToClimb,
    Climb
}
