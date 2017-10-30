using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private SpriteRenderer renderer;

    public int damage;
    public float lifeTime;
    public float speed;
    public string targetTag;
    public AmmoHit ammoHitObject;

    [HideInInspector] public Vector3 flyDirection;

    void Awake()
    {
        StartCoroutine(Timer(lifeTime));
        StartCoroutine("Move");
        renderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            var enemy = other.gameObject.GetComponent<Character>();
            enemy.TakeDamage(damage);

            Hit(true);
        }
        else if (other.CompareTag("SolidObject"))
        {
            Hit(false);
        }
    }

    private void Hit(bool targetHit)
    {
        StopCoroutine("Timer");
        StopCoroutine("Move");

        ammoHitObject.targetHit = targetHit;
        ammoHitObject.gameObject.SetActive(true);

        renderer.enabled = false;

        StartCoroutine(Timer(lifeTime / 2));
    }

    private IEnumerator Move()
    {
        while (true)
        {
            transform.Translate(flyDirection * speed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
