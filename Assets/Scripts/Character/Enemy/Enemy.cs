using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private SpriteRenderer spriteRenderer;
    private float fadeOutTime;

    public AnimationCurve fadeOutCurve;
    public float destructionDelay;

    public override void TakeDamage(int damageAmount, Vector2 hitPosition, Vector2 hitDirection)
    {
        HitManager.SpawnHitEffect(hitPosition, hitDirection, EffectType.Blood);
        health -= damageAmount;
        if (health <= 0)
        {
            controller.SetSimulated(false);
            animator.SetTrigger("Dead");
            inputManager.enabled = false;
            StartCoroutine(EnemyDestruction());
        }
    }

    protected virtual IEnumerator EnemyDestruction()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(destructionDelay);
        animator.SetTrigger("FadeOut");
        StartCoroutine("FadeOut");
    }

    protected override void SubscribeEvents()
    {
        base.SubscribeEvents();
        EventManager.StartListen(Constants.PlayerDeathEvent, PlayerDead);
    }

    protected override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        EventManager.StopListen(Constants.PlayerDeathEvent, PlayerDead);
    }

    private void PlayerDead()
    {
        inputManager.enabled = false;
    }

    private IEnumerator FadeOut()
    {
        while (true)
        {
            if (spriteRenderer.color.a > Mathf.Epsilon)
            {
                var color = spriteRenderer.color;
                color.a = fadeOutCurve.Evaluate(fadeOutTime);
                spriteRenderer.color = color;
                fadeOutTime += Time.deltaTime;
            }
            else
            {
                StopCoroutine("FadeOut");
                Destroy(this.gameObject, 1.0f);
            }
            yield return null;
        }
    }
}
