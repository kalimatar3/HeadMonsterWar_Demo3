using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public class EnemiesReciver : DameReciver
{
    protected EnemieCtrl EnemieCtrl;
    [SerializeField] protected bool DropGate;
    public override void ReBorn()
    {
        base.ReBorn();
        foreach(Transform element in EnemieCtrl.transform)
        {
            element.gameObject.SetActive(true);
        }
        this.EnemieCtrl.EnemieAct.gameObject.SetActive(true);
        if(this.EnemieCtrl.EnemieAct1 != null)
        {
            this.EnemieCtrl.EnemieAct1.gameObject.SetActive(true);
        }

        this.EnemieCtrl.GetComponent<BoxCollider>().enabled = true;
        this.EnemieCtrl.TrackPlayer.thisNav.enabled = true;
        this.EnemieCtrl.Model.transform.localPosition = Vector3.zero;
        DropGate = false;
        GetComponent<CapsuleCollider>().enabled = true;
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
        EffectSpawner.Instance.Spawn(CONSTEffect.EnemyDeadEffect,this.transform.position,this.transform.rotation);
        this.EnemieCtrl.TrackPlayer.thisNav.enabled = false;
        this.EnemieCtrl.EnemieAct.gameObject.SetActive(false);
        if(this.EnemieCtrl.EnemieAct1 != null)
        {
            this.EnemieCtrl.EnemieAct1.gameObject.SetActive(false);
        }
        this.EnemieCtrl.TrackPlayer.gameObject.SetActive(false);
        GetComponent<CapsuleCollider>().enabled = false;
        this.EnemieCtrl.GetComponent<BoxCollider>().enabled = false;
        if(!DropGate)
        {
            DropGate = true;
            SoundSpawner.Instance.Spawn(CONSTSoundsName.EnemyDead,this.transform.position,Quaternion.identity);
            this.EnemieCtrl.SpawnDropItem.spawnBuff();
        }
        yield return new WaitForSeconds(2f);
        this.EnemieCtrl.Model.DOMoveY(-2f,1f)
        .SetRelative()
        .SetEase(Ease.Linear);
        yield return new WaitForSeconds(1f);
        this.Dying();
    }
    protected override void Dying()
    {
        base.Dying();
        EnemiesSpawner.Instance.DeSpawnToPool(this.transform.parent);
    }
}
