using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInputManager : MonoBehaviour, IInputManager
{
    private const float sqrDistanceThreshold = 0.01f;

    private StateInput stateInput;
    private NavigationPoint currentPoint;
    private CharacterController2D controller;

    private RaycastHit2D[] raycastHits;

    private bool stopMoving;

    public GroundCheck groundCheck;
    public float moveDelayOnShoot;
    public float viewDistance;
    public LayerMask whoIsPlayer;
    public NavigationPoint[] navigationPoints;

    public bool GetButtonStatus(string button)
    {
        var hitsCount = Physics2D.RaycastNonAlloc(transform.position, controller.GetFacingDirection(), raycastHits, viewDistance, whoIsPlayer);
        for (int i = 0; i < hitsCount; i++)
        {
            if (raycastHits[i].rigidbody && raycastHits[i].rigidbody.CompareTag(Constants.PlayerTag))
            {
                if (!stopMoving)
                {
                    StartCoroutine("InteruptMovement");
                }
                return true;
            }
        }
        return false;
    }

    public void FixedUpdateInput()
    {
        stateInput.grounded = groundCheck.IsGrounded();
    }

    public StateInput GetStateInput()
    {
        return stateInput;
    }

    public void ClearInput()
    {
        stateInput.Clear();
    }

    void Awake()
    {
        stateInput = new StateInput();
        stateInput.Clear();

        controller = GetComponent<CharacterController2D>();

        raycastHits = new RaycastHit2D[5];
        currentPoint = navigationPoints[0];
    }

    void Update()
    {
        stateInput.stopMoving = stopMoving;
        stateInput.horizontal = 0.0f;
        if (!stopMoving)
        {
            if (NextPointReached())
            {
                currentPoint = currentPoint.nextPoint;
            }
            stateInput.horizontal = 1.0f * Mathf.Sign(currentPoint.nextPoint.transform.position.x - transform.position.x);
        }
    }

    private bool NextPointReached()
    {
        return (currentPoint.nextPoint.transform.position - transform.position).sqrMagnitude < sqrDistanceThreshold;
    }

    private IEnumerator InteruptMovement()
    {
        stopMoving = true;
        yield return new WaitForSeconds(moveDelayOnShoot);
        stopMoving = false;
    }
}
