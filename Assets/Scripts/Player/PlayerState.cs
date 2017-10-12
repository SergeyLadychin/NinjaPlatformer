using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private StateInput stateInput;

    private const float groundCheckRadius = 0.1f;

    private Transform groundCheck;
    private Transform climbCheck;
    private Transform headCheck;
    private Animator animator;
    private Rigidbody2D _rigidbody2D;

    public bool allowDoubleJump;
    public LayerMask whatIsGround;
    public LayerMask whatIsClimbArea;
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
        var stateInputProvider = new StateInputProvider();
        stateInputProvider.Set(stateInput);

        allStates = new Dictionary<StateType, IState>();
        allStates.Add(StateType.Idle, new IdleState(characterController, stateInputProvider));
        allStates.Add(StateType.Run, new RunState(characterController, stateInputProvider));
        allStates.Add(StateType.Jump, new JumpState(characterController, stateInputProvider));
        allStates.Add(StateType.FreeFall, new InAirState(characterController, stateInputProvider));
        allStates.Add(StateType.PrepareToClimb, new PrepareToClimbState(characterController, stateInputProvider));
        allStates.Add(StateType.ClimbJumpOff, new ClimbJumpOffState(characterController, stateInputProvider));
        allStates.Add(StateType.Climb, new ClimbState(characterController, stateInputProvider));

        if (allowDoubleJump)
        {
            allStates.Add(StateType.DoubleJump, new DoubleJumpState(characterController, stateInputProvider));
        }

        statesHierarchy = new List<IState>();
        statesHierarchy.Add(allStates[StateType.Climb]);
        statesHierarchy.Add(allStates[StateType.Jump]);
        if (allowDoubleJump)
        {
            statesHierarchy.Add(allStates[StateType.DoubleJump]);
        }
        statesHierarchy.Add(allStates[StateType.FreeFall]);
        statesHierarchy.Add(allStates[StateType.Idle]);

        animator = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheck");
        climbCheck = transform.Find("ClimbCheck");
        headCheck = transform.Find("HeadCheck");
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Check if flag was dropped instead of just setting field 'jump' every Update call,
        //because Update can be called several times before FixedUpdate will be called.
        if (!stateInput.jump)
        {
            stateInput.jump = Input.GetButtonDown("Jump");
        }
    }

    void FixedUpdate()
    {
        stateInput.horizontal = Input.GetAxis("Horizontal");
        stateInput.vertical = Input.GetAxis("Vertical");

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
        colliders = Physics2D.OverlapCircleAll(climbCheck.position, climbCheckRadius, whatIsClimbArea);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                inClimbArea = true;
                whereCanClimb = colliders[i].gameObject.transform.Find("ClimbPosition").position;
            }
        }

        bool climbTopReached = false;
        if (inClimbArea)
        {
            colliders = Physics2D.OverlapCircleAll(headCheck.position, groundCheckRadius);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.name == "LadderTop")
                {
                    climbTopReached = true;
                }
            }
        }

        stateInput.inClimbArea = inClimbArea;
        stateInput.climbTopReached = climbTopReached;
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
                if (nextState == currentState.Type)
                    break;

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
        //Debug.Log(string.Format("Current state {0}", currentState.Type));
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
    DoubleJump,
    FreeFall,
    PrepareToClimb,
    ClimbJumpOff,
    Climb
}
