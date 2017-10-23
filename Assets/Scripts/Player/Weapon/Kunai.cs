using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public float lifeTime;
    public float speed;
    [HideInInspector]public Vector3 flyDirection;

    void Awake()
    {
        StartCoroutine("Timer");
    }

    void Update()
    {
        transform.Translate(flyDirection * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage();

            StopCoroutine("Timer");
            Destroy(gameObject);
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
