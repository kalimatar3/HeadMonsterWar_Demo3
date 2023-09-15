using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufftoPlayer : DameDealer
{
    protected override void SendDametoObj(Transform obj)
    {
        base.SendDametoObj(obj);
        BuffSpawner.Instance.DeSpawnToPool(this.transform.parent);
        BuffManager.Instance.DeductCurrentNumberofBuff(this.transform.parent.name);
    }
}
