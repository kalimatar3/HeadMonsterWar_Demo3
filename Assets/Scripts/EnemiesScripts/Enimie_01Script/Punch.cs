using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : EnemieAct
{
    protected void punching()
    {
        timer += Time.deltaTime * 1f;
        if(this.timer > timerate && gate)
       {
            timer  = 0;
            gate = false;
            EnemieActSpawner.Instance.Spawn(EnemieActSpawner.ActEnum.Punch.ToString(),this.transform.parent.position,this.transform.parent.rotation);
       }
    }
    protected override void Doing()
    {
        base.Doing();
        this.punching();
    }
}
