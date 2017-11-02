using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INavigationPointMoveAction
{
    void ApplyToObject(Transform objectPosition, ref StateInput objectInput);
    bool ChangePoint(Transform objectPosition);
    void Reset();
}
