using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpiderPunch : EnemieAct
{
    protected void punching()
    {
        timer += Time.deltaTime;
        if(this.timer >= timerate && gate)
       {
            timer  = 0;
            gate = false;
            SoundSpawner.Instance.Spawn(CONSTSoundsName.SpiderAttack,transform.parent.position,Quaternion.identity);
            EnemieActSpawner.Instance.Spawn(EnemieActSpawner.ActEnum.BossPunch.ToString(),this.transform.parent.position,this.transform.parent.rotation);
       }
    }
    protected override void Doing()
    {
        base.Doing();
        this.punching();
    }
}
