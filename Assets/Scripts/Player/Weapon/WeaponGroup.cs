using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponGroup
{
    public string name;
    public string activationButton;
    public bool deactivateOnFire;
    public WeaponItem[] items;

    public void Init(CharacterController2D characterController, Animator characterAnimator)
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].weapon.Init(characterController, characterAnimator);
        }
    }

    public void Activate()
    {
        items[0].weapon.Activate();
    }

    public void Deactive()
    {
        items[0].weapon.Deactivate();
    }

    public void UpdateInput()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].UpdateInput();
        }
    }

    public bool CheckUserInput()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].CheckInput())
            {
                return true;
            }
        }
        return false;
    }

    public float Fire()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].CheckInput())
            {
                var cooldown = items[i].weapon.Fire();
                items[i].ClearInput();
                return cooldown;
            }
        }
        return -1.0f;
    }
}
