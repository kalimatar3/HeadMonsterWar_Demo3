using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDealtoPlayer : EnemyDealToPlayer
{
    protected override void OnTriggerEnter(Collider other)
    {
        this.SendDametoObj(other.transform);
    }
}
