using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    StateType Type { get; }

    void Enter();
    bool TryMakeTransition(StateInput input, out StateType newState);
    void Update();
    void Exit();
}
