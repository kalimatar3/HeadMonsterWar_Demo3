using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealToPlayer : DameDealer
{
    protected override void SendDametoObj(Transform obj)
    {
        PlayerReciver playerReciver =  obj.transform.GetComponent<PlayerReciver>();
        if(playerReciver != PlayerController.Instance.PlayerReciver)   return;
        playerReciver.DeductHp(this.dealnumber);
        base.SendDametoObj(obj);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if(other == null || other.tag != CONSTtag.Playertag) SoundSpawner.Instance.Spawn(CONSTSoundsName.AttackMiss,this.transform.parent.position,Quaternion.identity);
        base.OnTriggerEnter(other);
    }
}
