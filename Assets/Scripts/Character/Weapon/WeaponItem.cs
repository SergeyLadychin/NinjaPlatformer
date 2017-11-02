using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class WeaponItem
{
    private bool buttonPressed = false;

    public string name;
    public Weapon weapon;
    public string fireButton;
    public StateType[] avaliableInStates;

    public void UpdateInput()
    {
        if (!buttonPressed)
        {
            buttonPressed = weapon.inputManager.GetButtonStatus(fireButton);
        }
    }

    public bool CheckInput(StateType currentState)
    {
        return buttonPressed && avaliableInStates.Contains(currentState);
    }

    public void ClearInput()
    {
        buttonPressed = false;
    }

    public bool IsAvaliable()
    {
        return weapon.IsAvaliable();
    }
}
