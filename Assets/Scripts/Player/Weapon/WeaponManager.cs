using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private const int defaultGroupIndex = 0;
    private int activeGroupIndex = 0;
    private float cooldown = -1.0f;

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

        //Update input
	    var activeGroup = GetActiveGroup();
        activeGroup.UpdateInput();

        //Update cooldown
        if (cooldown > 0.0f)
        {
            cooldown -= Time.deltaTime;
        }
	}

    public bool CheckUserInput()
    {
        if (cooldown < 0.0f)
        {
            var activeGroup = GetActiveGroup();
            return activeGroup.CheckUserInput();
        }
        return false;
    }

    public void Fire()
    {
        var activeGroup = GetActiveGroup();
        var weaponCooldown = activeGroup.Fire();
        if (weaponCooldown > 0.0f && activeGroup.deactivateOnFire)
        {
            activeGroup.Deactive();
            activeGroupIndex = defaultGroupIndex;
        }
        cooldown = weaponCooldown;
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
