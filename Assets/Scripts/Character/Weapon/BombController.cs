using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    public int damage;
    public float detonationRadius;
    public float detonationTimer;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine("SetBombTimer");
    }

    public void ThrowBomb(Vector3 velocity)
    {
        _rigidbody2D.velocity += (Vector2) velocity;
    }

    IEnumerator SetBombTimer()
    {
        yield return new WaitForSeconds(detonationTimer);
        DetonateBomb();
    }

    void DetonateBomb()
    {
        _rigidbody2D.simulated = false;
        _rigidbody2D.velocity = Vector2.zero;
        var colliders = Physics2D.OverlapCircleAll(transform.position, detonationRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            var character = colliders[i].GetComponent<Character>();
            if (character)
            {
                character.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
