using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSpawnHpbar : DespawnbyObj
{
    protected override void DeSpawnObjects()
    {
        base.DeSpawnObjects();
        HpbarSpawner.Instance.DeSpawnToPool(this.transform.parent);
    }
}
