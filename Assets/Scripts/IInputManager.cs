using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputManager
{
    void FixedUpdateInput();
    StateInput GetStateInput();
    void ClearInput();
    bool GetButtonStatus(string button);
}
