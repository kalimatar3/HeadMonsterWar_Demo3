using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActDespawn : Despawnbytime
{
    protected override void DeSpawnObjects()
    {
        EnemieActSpawner.Instance.DeSpawnToPool(this.transform.parent);
    }
}
