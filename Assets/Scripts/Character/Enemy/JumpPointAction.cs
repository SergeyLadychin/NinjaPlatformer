using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPointAction : MonoBehaviour, INavigationPointMoveAction
{
    private bool actionCompleted;

    public void ApplyToObject(Transform objectPosition, ref StateInput stateInput)
    {
        if (!actionCompleted)
        {
            stateInput.jump = true;
            actionCompleted = true;
        }
    }

    public bool ChangePoint(Transform objectPosition)
    {
        return actionCompleted;
    }

    public void Reset()
    {
        actionCompleted = false;
    }
}
