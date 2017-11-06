using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputManager
{
    void FixedUpdateInput();
    StateInput GetStateInput();
    void ClearInput();
    bool GetFireButtonStatus(Vector3 weaponPosition, string button);
    bool GetActivateButtonStatus(string button);
    Vector3 GetMousePosition(Vector3 weaponPosition);
}
