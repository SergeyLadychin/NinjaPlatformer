using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Action<GameObject> disableAction;

    public int damage;
    public float lifeTime;
    public float speed;
    public string targetTag;
    public ObjectGetter ammoHitGetter;

    [HideInInspector] public Vector3 flyDirection;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        spriteRenderer.enabled = true;
        body.simulated = true;

        StartCoroutine(Timer(lifeTime));
        StartCoroutine("Move");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            var enemy = other.gameObject.GetComponent<Character>();
            enemy.TakeDamage(damage);

            Hit(ObjectHit.Body);
        }
        else if (other.CompareTag("SolidObject"))
        {
            Hit(ObjectHit.SolidObject);
        }
    }

    public void SetDisableFunc(Action<GameObject> disableAction)
    {
        this.disableAction = disableAction;
    }

    private void Hit(ObjectHit objectHit)
    {
        StopCoroutine("Timer");
        StopCoroutine("Move");

        var ammoHitObject = ammoHitGetter.Get<HitAnimation>(transform.position, transform.rotation);
        ammoHitObject.Init(objectHit, flyDirection);

        spriteRenderer.enabled = false;
        body.simulated = false;
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
        StopCoroutine("Move");
        disableAction(gameObject);
    }
}
