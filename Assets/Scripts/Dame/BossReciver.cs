using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReciver : EnemiesReciver
{
    protected override void Dying()
    {
        EffectSpawner.Instance.Spawn("BossDieExplosion",this.transform.parent.position,this.transform.parent.rotation);
        SoundSpawner.Instance.Spawn(CONSTSoundsName.BossDead,this.transform.position,Quaternion.identity);
        base.Dying();
    }
}
