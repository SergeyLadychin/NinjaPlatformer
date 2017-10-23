using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite openDoor;
    public Sprite lockedDoor;
    public Sprite closedDoor;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closedDoor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            if (!IsLocked())
            {
                OpenDoor();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            if (!IsLocked())
            {
                CloseDoor();
            }
        }
    }

    public void OpenDoor()
    {
        spriteRenderer.sprite = openDoor;
    }

    public void CloseDoor()
    {
        spriteRenderer.sprite = closedDoor;
    }

    public void LockDoor()
    {
        spriteRenderer.sprite = lockedDoor;
    }

    public void UnlockDoor()
    {
        CloseDoor();
    }

    private bool IsLocked()
    {
        return spriteRenderer.sprite.Equals(lockedDoor);
    }
}
