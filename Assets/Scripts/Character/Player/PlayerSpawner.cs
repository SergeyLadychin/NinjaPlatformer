using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private Animator animator;

    public Player player;
    public float delayBeforeSpawn;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        Invoke("StartAnimator", delayBeforeSpawn);
    }

    public void Spawn()
    {
        player.transform.position = transform.position;
        player.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void StartAnimator()
    {
        animator.enabled = true;
    }
}
