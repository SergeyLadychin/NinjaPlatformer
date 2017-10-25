using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    private Animator animator;
    private CharacterController2D characterController;
    private IInputManager inputManager;

    public bool allowDoubleJump;

    private IState currentState
    {
        get { return statesHierarchy[statesHierarchy.Count - 1]; }
        set { statesHierarchy[statesHierarchy.Count - 1] = value; }
    }

    private Dictionary<StateType, IState> allStates;
    private List<IState> statesHierarchy;

    void Awake()
    {
        inputManager = GetComponent<IInputManager>();
        characterController = GetComponent<CharacterController2D>();

        allStates = new Dictionary<StateType, IState>();
        allStates.Add(StateType.Idle, new IdleState(characterController, inputManager));
        allStates.Add(StateType.Run, new RunState(characterController, inputManager));
        allStates.Add(StateType.Jump, new JumpState(characterController, inputManager));
        allStates.Add(StateType.FreeFall, new InAirState(characterController, inputManager));
        allStates.Add(StateType.PrepareToClimb, new PrepareToClimbState(characterController, inputManager));
        allStates.Add(StateType.ClimbJumpOff, new ClimbJumpOffState(characterController, inputManager));
        allStates.Add(StateType.Climb, new ClimbState(characterController, inputManager));
        allStates.Add(StateType.Attack, new AttackState(characterController, inputManager, GetComponent<WeaponManager>()));

        if (allowDoubleJump)
        {
            allStates.Add(StateType.DoubleJump, new DoubleJumpState(characterController, inputManager));
        }

        statesHierarchy = new List<IState>();
        statesHierarchy.Add(allStates[StateType.Attack]);
        statesHierarchy.Add(allStates[StateType.Climb]);
        statesHierarchy.Add(allStates[StateType.Jump]);
        if (allowDoubleJump)
        {
            statesHierarchy.Add(allStates[StateType.DoubleJump]);
        }
        statesHierarchy.Add(allStates[StateType.FreeFall]);
        statesHierarchy.Add(allStates[StateType.Idle]);

        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        inputManager.FixedUpdateInput();
        var stateInput = inputManager.GetStateInput();

        var velocity = characterController.GetVelocity();

        animator.SetBool("Grounded", stateInput.grounded);
        animator.SetFloat("hSpeed", Mathf.Abs(velocity.x));
        animator.SetFloat("vSpeed", velocity.y);
        Debug.LogFormat("Horizontal {0}, Vertical {1}, InClimbA {2}", stateInput.horizontal, stateInput.vertical, stateInput.inClimbArea);
        UpdateState();
        inputManager.ClearInput();
    }

    private void UpdateState()
    {
        for (int i = 0; i < statesHierarchy.Count; i++)
        {
            StateType nextState;
            if (statesHierarchy[i].TryMakeTransition(currentState.Type, out nextState))
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
        Debug.Log(string.Format("Current state {0}", currentState.Type));
        currentState.Update();
    }
}

public enum StateType
{
    Idle,
    Run,
    Jump,
    DoubleJump,
    FreeFall,
    PrepareToClimb,
    ClimbJumpOff,
    Climb,
    Attack
}
