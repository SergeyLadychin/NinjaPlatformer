using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class WeaponGroup
{
    private int validatedItemIndex;
    private IInputManager inputManager;

    public string name;
    public string activationButton;
    public bool deactivateOnFire;
    public WeaponItem[] items;

    public void Init(CharacterController2D characterController, Animator characterAnimator, IInputManager inputManager)
    {
        this.inputManager = inputManager;
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

    public bool CheckUserInput(StateType currentState)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].CheckInput(currentState))
            {
                validatedItemIndex = i;
                return true;
            }
        }
        validatedItemIndex = -1;
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
        if (validatedItemIndex != -1)
        {
            var cooldown = items[validatedItemIndex].weapon.OnFire();
            items[validatedItemIndex].ClearInput();
            eventData = new FireEventData(this, validatedItemIndex);
            return cooldown;
        }
        return -1.0f;
    }

    public void Fire(int itemIndex)
    {
        items[itemIndex].weapon.Fire();
    }

    public bool IsAvaliable()
    {
        return items.Any(i => i.IsAvaliable());
    }

    public bool GetActivateButtonStatus()
    {
        return inputManager.GetActivateButtonStatus(activationButton);
    }
}
