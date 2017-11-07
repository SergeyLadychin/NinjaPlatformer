using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    protected override IEnumerator EnemyDestruction()
    {
        yield return new WaitForSeconds(destructionDelay);
        HitManager.SpawnHitEffect(transform.position, Vector3.right, EffectType.BombExplosion);

        HitManager.SpawnHitEffect(transform.position + new Vector3(-0.4f, -0.6f, 0.0f), Vector3.left, EffectType.Blood);
        HitManager.SpawnHitEffect(transform.position + new Vector3(-0.4f, 0.0f, 0.0f), Vector3.left, EffectType.Blood);
        HitManager.SpawnHitEffect(transform.position + new Vector3(-0.4f, 0.6f, 0.0f), Vector3.left, EffectType.Blood);
        HitManager.SpawnHitEffect(transform.position + new Vector3(0.4f, -0.6f, 0.0f), Vector3.right, EffectType.Blood);
        HitManager.SpawnHitEffect(transform.position + new Vector3(0.4f, 0.0f, 0.0f), Vector3.right, EffectType.Blood);
        HitManager.SpawnHitEffect(transform.position + new Vector3(0.4f, 0.6f, 0.0f), Vector3.right, EffectType.Blood);

        EventManager.TriggerEvent(Constants.DeactivatePlayerControlEvent);
        EventManager.TriggerEvent(Constants.ShowCreditsEvent);

        Destroy(this.gameObject);
    }
}
