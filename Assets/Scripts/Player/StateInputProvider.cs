using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInputProvider : IStateInputProvider
{
    private StateInput input;

    public void Set(StateInput input)
    {
        this.input = input;
    }

    public StateInput Get()
    {
        return input;
    }
}
