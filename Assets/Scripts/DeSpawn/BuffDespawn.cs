using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDespawn : Despawnbytime
{
    protected override void DeSpawnObjects()
    {
        BuffSpawner.Instance.DeSpawnToPool(this.transform.parent);
        BuffManager.Instance.DeductCurrentNumberofBuff(this.transform.parent.name);
    } 
}
