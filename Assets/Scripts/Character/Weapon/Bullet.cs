using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Action<GameObject> disableAction;

    private bool targetHitted;
    private float lifeTime;
    private Vector3 flyDirection;

    public int damage;
    public float speed;
    public string targetTag;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!targetHitted && other.CompareTag(targetTag))
        {
            targetHitted = true;

            var enemy = other.gameObject.GetComponent<Character>();
            enemy.TakeDamage(damage, transform.position, flyDirection);

            Hit();
        }
        else if (other.CompareTag("SolidObject"))
        {
            Hit();
            HitManager.SpawnHitEffect(transform.position, flyDirection, EffectType.SolidObjectHit);
        }
    }

    public void Init(Vector3 flyDirection, float lifeTime, Action<GameObject> disableAction)
    {
        this.flyDirection = flyDirection;
        this.lifeTime = lifeTime;
        this.disableAction = disableAction;

        targetHitted = false;

        spriteRenderer.enabled = true;
        body.simulated = true;

        StartCoroutine(Timer(lifeTime));
        StartCoroutine("Move");
    }

    private void Hit()
    {
        StopCoroutine("Timer");
        StopCoroutine("Move");

        spriteRenderer.enabled = false;
        body.simulated = false;
    }

    private IEnumerator Move()
    {
        while (true)
        {
            transform.Translate(flyDirection * speed * Time.deltaTime, Space.World);
            yield return null;
        }
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        StopCoroutine("Move");
        disableAction(gameObject);
    }
}
