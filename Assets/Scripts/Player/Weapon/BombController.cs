using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

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
        Destroy(gameObject);
    }
}
