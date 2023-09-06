using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTouched : DameDealer
{
    protected override void SendDametoObj(Transform obj)
    {
        CanTouchENReciver canTouchENReciver = obj.GetComponent<CanTouchENReciver>();
        if(canTouchENReciver == null)     return;
        canTouchENReciver.Touched = true;
    }
    protected override void OnTriggerEnter(Collider other)
    {
    }
    protected void OnCollisionStay(Collision collision)
    {
        this.SendDametoObj(collision.transform);
    }
 }
