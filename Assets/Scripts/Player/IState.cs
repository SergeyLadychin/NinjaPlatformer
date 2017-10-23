using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    StateType Type { get; }

    void Enter();
    bool TryMakeTransition(StateType current, out StateType newState);
    void Update();
    void Exit();
}
