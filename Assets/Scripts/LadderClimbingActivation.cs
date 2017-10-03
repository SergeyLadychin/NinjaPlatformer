using System.Collections;
using System.Collections.Generic;
using GravityAdventure;
using UnityEngine;

public class LadderClimbingActivation : MonoBehaviour
{
    private Transform ladderCenter;
    private PlayerCharacterController characterController;

    void Awake()
    {
        ladderCenter = transform.Find("LadderCenter");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            characterController = other.GetComponent<PlayerCharacterController>();
            characterController.state.UpdateCanClimb(true, ladderCenter.position);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        characterController.state.UpdateCanClimb(false, Vector3.zero);
    }
}
