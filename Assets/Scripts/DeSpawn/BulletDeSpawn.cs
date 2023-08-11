using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeSpawn : DespawnbyDistance
{
    protected override void DeSpawnObjects()
    {
        BulletSpawner.Instance.DeSpawnToPool(this.transform.parent);
    } 
}
