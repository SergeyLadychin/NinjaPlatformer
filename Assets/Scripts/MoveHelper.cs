using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHelper
{
    public static float ReverseClamp01(float value)
    {
        if (Mathf.Abs(value) < 0.001f)
            return 0.0f;
        return Mathf.Sign(value) * 1.0f;
    }
}
