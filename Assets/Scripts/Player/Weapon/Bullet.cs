using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime;
    public float speed;
    public string targetTag;

    [HideInInspector]public Vector3 flyDirection;

    void Awake()
    {
        StartCoroutine("Timer");
        StartCoroutine("Move");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            var enemy = other.gameObject.GetComponent<Character>();
            enemy.TakeDamage();

            StopCoroutine("Timer");
            StopCoroutine("Move");
            Destroy(gameObject);
        }
    }

    private IEnumerator Move()
    {
        while (true)
        {
            transform.Translate(flyDirection * speed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
