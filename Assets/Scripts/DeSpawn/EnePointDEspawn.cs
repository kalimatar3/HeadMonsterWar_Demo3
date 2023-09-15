using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnePointDEspawn : DespawnbyObj
{
    protected override void DeSpawnObjects()
    {
        base.DeSpawnObjects();
        BossPointSpawner.Instance.DeSpawnToPool(this.transform.parent);
    }
}
