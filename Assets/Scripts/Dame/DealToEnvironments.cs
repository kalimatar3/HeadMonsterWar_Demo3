using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealToEnvironments : DameDealer
{
    public string ExplosionHitName;
    protected override void SendDametoObj(Transform obj)
    {
        base.SendDametoObj(obj);
        EnvironmentReciver environmentReciver  = obj.GetComponent<EnvironmentReciver>();
        if(environmentReciver== null) return;
        EffectSpawner.Instance.Spawn(ExplosionHitName,this.transform.position,this.transform.rotation);
        BulletSpawner.Instance.DeSpawnToPool(this.transform.parent); 
    }
}
