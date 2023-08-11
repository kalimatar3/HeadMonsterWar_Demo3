using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : EnemieAct
{
    protected void explode()
    {
        timer += Time.deltaTime * 1f;
        if(this.timer > timerate + 0.5f  && gate)
       {
        timer  = 0;
        gate = false;
        SoundSpawner.Instance.Spawn(CONSTSoundsName.Boom,this.transform.parent.position,Quaternion.identity);
        EnemieActSpawner.Instance.Spawn(EnemieActSpawner.ActEnum.Explode.ToString(),this.transform.parent.position,this.transform.parent.rotation);
        EnemiesSpawner.Instance.DeSpawnToPool(this.transform.parent);
       }
    }
    protected override void Doing()
    {
        base.Doing();
        this.explode();
    }
}
