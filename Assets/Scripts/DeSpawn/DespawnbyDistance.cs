using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnbyDistance : Despawn
{
    [SerializeField] protected Vector3 OriginPos;
    [SerializeField] public float DistanceToDepSpawn;
    protected  void OnEnable()
    {
        this.TakeOriginPos();
    }
    protected void TakeOriginPos()
    {
        OriginPos = this.transform.parent.position;
    }
    protected override bool CanDeSpawn()
    {
        float distance = (this.transform.parent.position - OriginPos).magnitude;
        if(distance <= DistanceToDepSpawn) return false;
        return true;
    } 
}
