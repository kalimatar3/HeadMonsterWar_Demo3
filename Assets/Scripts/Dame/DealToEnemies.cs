using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealToEnemies : DameDealer
{
    public string ExplosionHitName;
    [SerializeField] public bool CanThroughObj;
    protected override void SendDametoObj(Transform obj)
    {
        EnemiesReciver enemiesReciver =  obj.transform.GetComponent<EnemiesReciver>();
        if(enemiesReciver == null) return;
        enemiesReciver.DeductHp(this.dealnumber);
        EffectSpawner.Instance.Spawn(ExplosionHitName,this.transform.position,this.transform.rotation);
        if(!CanThroughObj) 
        {
            BulletSpawner.Instance.DeSpawnToPool(this.transform.parent);
        }
        base.SendDametoObj(obj);
    }
}
