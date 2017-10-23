using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class WeaponManager : MonoBehaviour
{
    private const int defaultGroupIndex = 0;
    private int activeGroupIndex = 0;

    public WeaponGroup[] weaponGroups;

	void Start ()
	{
	    var characterController = GetComponent<CharacterController2D>();
	    var characterAnimator = GetComponent<Animator>();

	    if (weaponGroups.Length == 0)
	    {
	        Debug.LogError("Weapon manager must have at least one weapon group.");
	    }

	    for (int i = 0; i < weaponGroups.Length; i++)
	    {
	        weaponGroups[i].Init(characterController, characterAnimator);
	    }
	}

	void Update ()
	{
        UpdateActive();

	    var activeGroup = GetActiveGroup();
        activeGroup.UpdateInput();
	}

    public bool CheckUserInput()
    {
        var activeGroup = GetActiveGroup();
        return activeGroup.CheckUserInput();
    }

    public float Fire()
    {
        var activeGroup = GetActiveGroup();
        var cooldown = activeGroup.Fire();
        if (cooldown > 0.0f && activeGroup.deactivateOnFire)
        {
            activeGroup.Deactive();
            activeGroupIndex = defaultGroupIndex;
        }
        return cooldown;
    }

    private void UpdateActive()
    {
        var prevActiveGroupIndex = activeGroupIndex;
        bool buttonPressed = false;
        //Start iterate from second group,
        //because first group is always default and don't have activationButton
        //default group is active if others inactive.
        for (int i = 1; i < weaponGroups.Length; i++)
        {
            if (Input.GetButtonDown(weaponGroups[i].activationButton))
            {
                activeGroupIndex = i;
                buttonPressed = true;
                break;
            }
        }
        if (prevActiveGroupIndex != activeGroupIndex)
        {
            weaponGroups[prevActiveGroupIndex].Deactive();
            weaponGroups[activeGroupIndex].Activate();
        }
        else if (buttonPressed)
        {
            weaponGroups[activeGroupIndex].Deactive();
            activeGroupIndex = defaultGroupIndex;
        }
    }

    private WeaponGroup GetActiveGroup()
    {
        return weaponGroups[activeGroupIndex];
    }
}
