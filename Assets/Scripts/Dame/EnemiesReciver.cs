using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesReciver : DameReciver
{
    protected EnemieCtrl EnemieCtrl;
    public override void ReBorn()
    {
        base.ReBorn();
        foreach(Transform element in EnemieCtrl.transform)
        {
            element.gameObject.SetActive(true);
        }
    }
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
    protected override IEnumerator Dead()
    {
        yield return new WaitUntil(predicate:()=>
        {
            return Candead();
        });
        this.EnemieCtrl.TrackPlayer.thisNav.speed = 0;
        this.EnemieCtrl.EnemieAct.gameObject.SetActive(false);
        this.EnemieCtrl.TrackPlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.25f);
        this.Dying();
    }

    protected override void Dying()
    {
        base.Dying();
        EnemiesSpawner.Instance.DeSpawnToPool(this.transform.parent);
        EffectSpawner.Instance.Spawn("EnemyDieExplosion",this.transform.position,this.transform.rotation);
        this.EnemieCtrl.SpawnDropItem.spawnBuff();
    }
}
