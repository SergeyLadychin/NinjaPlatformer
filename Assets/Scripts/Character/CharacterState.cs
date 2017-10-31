using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    private Animator animator;
    private CharacterController2D characterController;
    private IInputManager inputManager;

    private IState currentState
    {
        get { return statesHierarchyList[statesHierarchyList.Count - 1]; }
        set { statesHierarchyList[statesHierarchyList.Count - 1] = value; }
    }

    private Dictionary<StateType, IState> allStates;
    private List<IState> statesHierarchyList;

    public StateType[] characterStates;
    public StateType[] statesHierarchy;

    void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<IInputManager>();
        characterController = GetComponent<CharacterController2D>();

        allStates = new Dictionary<StateType, IState>();
        for (int i = 0; i < characterStates.Length; i++)
        {
            allStates.Add(characterStates[i], CreateState(characterStates[i]));
        }

        statesHierarchyList = new List<IState>();
        for (int i = 0; i < statesHierarchy.Length; i++)
        {
            statesHierarchyList.Add(allStates[statesHierarchy[i]]);
        }
    }

    void FixedUpdate()
    {
        inputManager.FixedUpdateInput();
        var stateInput = inputManager.GetStateInput();

        var velocity = characterController.GetVelocity();

        animator.SetBool("Grounded", stateInput.grounded);
        animator.SetFloat("hSpeed", Mathf.Abs(velocity.x));
        animator.SetFloat("vSpeed", velocity.y);

        UpdateState();
        inputManager.ClearInput();
    }

    private void UpdateState()
    {
        for (int i = 0; i < statesHierarchyList.Count; i++)
        {
            StateType nextState;
            if (statesHierarchyList[i].TryMakeTransition(currentState.Type, out nextState))
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

    private IState CreateState(StateType stateType)
    {
        switch (stateType)
        {
            case StateType.Idle:
                return new IdleState(characterController, inputManager);
            case StateType.Run:
                return new RunState(characterController, inputManager);
            case StateType.Jump:
                return new JumpState(characterController, inputManager);
            case StateType.DoubleJump:
                return new DoubleJumpState(characterController, inputManager);
            case StateType.FreeFall:
                return new InAirState(characterController, inputManager);
            case StateType.PrepareToClimb:
                return new PrepareToClimbState(characterController, inputManager);
            case StateType.ClimbJumpOff:
                return new ClimbJumpOffState(characterController, inputManager);
            case StateType.Climb:
                return new ClimbState(characterController, inputManager, animator);
            case StateType.Attack:
                return new AttackState(characterController, inputManager, GetComponent<WeaponManager>());
            default:
                Debug.LogErrorFormat("Undefinded state {0}.", stateType);
                break;
        }
        return null;
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
