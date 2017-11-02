using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private const int defaultGroupIndex = 0;
    private int activeGroupIndex = defaultGroupIndex;
    private bool cooldownFree = true;
    private bool animationFree = true;
    private FireEventData fireEventData;

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
        if (CanFire())
        {
            UpdateActive();
        }

        //Update input
	    var activeGroup = GetActiveGroup();
        activeGroup.UpdateInput();
	}

    public bool CheckUserInput(StateType currentState)
    {
        if (CanFire())
        {
            var activeGroup = GetActiveGroup();
            return activeGroup.CheckUserInput(currentState);
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
        var weaponCooldown = activeGroup.OnFire(out fireEventData);
        if (weaponCooldown > 0.0f)
        {
            animationFree = false;
            if (activeGroup.deactivateOnFire)
            {
                activeGroup.Deactive();
                activeGroupIndex = defaultGroupIndex;
            }
            StartCoroutine(ProcessCooldown(weaponCooldown));
        }
    }

    //FireEvent and FinishFireEvent functions used in pair to syncronize hit and character animation.

    //Function that must be added to appropriate animation clip(using event) to perform attack
    public void FireEvent(AnimationEvent animEvent)
    {
        fireEventData.group.items[fireEventData.itemIndex].weapon.Fire();
    }

    //Function that must be added to the end of animation clip(using event) of perfomed attack
    public void FinishFireEvent(AnimationEvent animEvent)
    {
        animationFree = true;
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
        cooldownFree = false;
        yield return new WaitForSeconds(cooldownTime);
        cooldownFree = true;
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
            if (weaponGroups[i].IsAvaliable() && Input.GetButtonDown(weaponGroups[i].activationButton))
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

    private bool CanFire()
    {
        return cooldownFree && animationFree;
    }
}

public struct FireEventData
{
    public WeaponGroup group;
    public int itemIndex;

    public FireEventData(WeaponGroup weaponGroup, int weaponIndex)
    {
        group = weaponGroup;
        itemIndex = weaponIndex;
    }
}
