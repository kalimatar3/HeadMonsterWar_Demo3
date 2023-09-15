using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnLazerHit : DespawnbyObj
{
    protected override void DeSpawnObjects()
    {
        base.DeSpawnObjects();
        EnemieActSpawner.Instance.DeSpawnToPool(this.transform.parent);
    }
}
