using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiSpawner : Weapon
{
    public GameObject kunaiPrefab;

    public override float Fire()
    {
        var kunaiObject = Instantiate(kunaiPrefab, transform.position, transform.rotation);
        var kunai = kunaiObject.GetComponent<Kunai>();
        kunai.flyDirection = controller.GetFacingDirection();
        return cooldown;
    }
}

