using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActbyDis : Act
{
    public float DisToAct;
    protected Transform Oritrans;
    [SerializeField]protected float Dis;
    protected override bool CanDo()
    {
        if(Oritrans == null) return false;
        this.Dis =  (this.Oritrans.position - this.transform.parent.position).magnitude;
        if(Dis > DisToAct) return false;
        return true; 
    }
}
