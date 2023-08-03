using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesReciver : DameReciver
{
    protected EnemieCtrl EnemieCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
    }
    protected virtual void LoadCtrl()
    {
        if(EnemieCtrl != null) return;
        EnemieCtrl = GetComponentInParent<EnemieCtrl>();
    }
    protected override void Dying()
    {
        base.Dying();
        EnemiesSpawner.Instance.DeSpawnToPool(this.transform.parent);
        EffectSpawner.Instance.Spawn("EnemyDieExplosion",this.transform.position,this.transform.rotation);
        this.EnemieCtrl.SpawnDropItem.spawnBuff();
    }
}
