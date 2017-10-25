using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private const int defaultGroupIndex = 0;
    private int activeGroupIndex = defaultGroupIndex;
    private bool canFire = true;

    public WeaponGroup[] weaponGroups;

	void Start ()
	{
	    var characterController = GetComponent<CharacterController2D>();
	    var characterAnimator = GetComponent<Animator>();
	    var inputManager = GetComponent<IInputManager>();

	    if (weaponGroups.Length == 0)
	    {
	        Debug.LogError("Weapon manager must have at least one weapon group.");
	    }

	    for (int i = 0; i < weaponGroups.Length; i++)
	    {
	        weaponGroups[i].Init(characterController, characterAnimator, inputManager);
	    }
	}

	void Update ()
	{
        UpdateActive();

        //Update input
	    var activeGroup = GetActiveGroup();
        activeGroup.UpdateInput();
	}

    public bool CheckUserInput()
    {
        if (canFire)
        {
            var activeGroup = GetActiveGroup();
            return activeGroup.CheckUserInput();
        }
        return false;
    }

    public void ClearUserInput()
    {
        var activeGroup = GetActiveGroup();
        activeGroup.ClearInput();
    }

    public void Fire()
    {
        var activeGroup = GetActiveGroup();
        var weaponCooldown = activeGroup.Fire();
        if (weaponCooldown > 0.0f)
        {
            if (activeGroup.deactivateOnFire)
            {
                activeGroup.Deactive();
                activeGroupIndex = defaultGroupIndex;
            }
            StartCoroutine("ProcessCooldown", weaponCooldown);
        }
    }

    public void SetDefaultGroup()
    {
        if (activeGroupIndex != defaultGroupIndex)
        {
            var activeGroup = GetActiveGroup();
            activeGroup.Deactive();
            activeGroupIndex = defaultGroupIndex;
        }
    }

    private IEnumerator ProcessCooldown(float cooldownTime)
    {
        canFire = false;
        yield return new WaitForSeconds(cooldownTime);
        canFire = true;
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
