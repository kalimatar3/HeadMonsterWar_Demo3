using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSpawnHpbar : Despawn
{
    protected override bool CanDeSpawn()
    {
        float Currenthp = GetComponent<EnemieHpbar>().CurrentHp;
        if(Currenthp > 0) return false;
        return true;
    }
    protected override void DeSpawnObjects()
    {
        base.DeSpawnObjects();
        HpbarSpawner.Instance.DeSpawnToPool(this.transform.parent);
    }
}
