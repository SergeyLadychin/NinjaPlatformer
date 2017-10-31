using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKillObject : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.PlayerTag) || other.CompareTag(Constants.EnemyTag))
        {
            var character = other.gameObject.GetComponent<Character>();
            character.TakeDamage(1000000);
        }
    }
}
