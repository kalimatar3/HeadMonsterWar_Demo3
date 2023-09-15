using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHP : BufftoPlayer
{
    protected override void SendDametoObj(Transform obj)
    {
        PlayerReciver playerReciver =  obj.transform.GetComponent<PlayerReciver>();
        if(playerReciver == null) return;
        playerReciver.RestoreHp(this.dealnumber);
        Transform speedupeffect = EffectSpawner.Instance.Spawn("HealingEffect",this.transform.parent.position,this.transform.parent.rotation);
        speedupeffect.GetComponentInChildren<EffectDespawn>().DespawnTime = 1f;
        SoundSpawner.Instance.Spawn(CONSTSoundsName.Healing,Vector3.zero,Quaternion.identity);
        base.SendDametoObj(obj);
    }
}
