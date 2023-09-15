using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllFollowObjinTime : AllfollowObj
{
    [SerializeField] protected float FollowTime,Timer;
    protected void OnEnable()
    {
        StartCoroutine(this.FollowDelay());
    } 
    protected IEnumerator FollowDelay()
    {
        yield return new WaitForSeconds(FollowTime);
        Obj = this.transform.parent.transform;
    }
}

