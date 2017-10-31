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

    public void Init(CharacterController2D characterController, Animator characterAnimator, IInputManager inputManager)
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].weapon.Init(characterController, characterAnimator, inputManager);
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

    public void ClearInput()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].ClearInput();
        }
    }

    public float OnFire(out FireEventData eventData)
    {
        eventData = new FireEventData(this, -1);
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].CheckInput())
            {
                var cooldown = items[i].weapon.OnFire();
                items[i].ClearInput();
                eventData = new FireEventData(this, i);
                return cooldown;
            }
        }
        return -1.0f;
    }

    public void Fire(int itemIndex)
    {
        items[itemIndex].weapon.Fire();
    }
}
