using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDespawn : Despawnbytime
{
    protected override void DeSpawnObjects()
    {
        EffectSpawner.Instance.DeSpawnToPool(this.transform.parent);
    }
}
