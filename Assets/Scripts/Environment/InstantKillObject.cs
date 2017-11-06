using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKillObject : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag(Constants.PlayerTag) || other.collider.CompareTag(Constants.EnemyTag))
        {
            var hitPosition = other.contacts[0].point;
            var nomal = other.contacts[0].normal;
            var character = other.gameObject.GetComponent<Character>();
            character.TakeDamage(1000000, hitPosition, -nomal);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.PlayerTag) || other.CompareTag(Constants.EnemyTag))
        {
            var character = other.gameObject.GetComponent<Character>();
            character.TakeDamage(1000000, other.transform.position, (other.transform.position - transform.position).normalized);
        }
    }
}
