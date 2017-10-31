using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponItem
{
    private bool buttonPressed = false;

    public string name;
    public Weapon weapon;
    public string fireButton;

    public void UpdateInput()
    {
        if (!buttonPressed)
        {
            buttonPressed = weapon.inputManager.GetButtonStatus(fireButton);
        }
    }

    public bool CheckInput()
    {
        return buttonPressed;
    }

    public void ClearInput()
    {
        buttonPressed = false;
    }
}
