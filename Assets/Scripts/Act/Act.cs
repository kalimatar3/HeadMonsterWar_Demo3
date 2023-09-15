using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Act : MyBehaviour
{
    protected virtual void Action()
    {
        if(!this.CanDo()) return;
        this.Doing();
    }
    protected virtual void Doing()
    {
        //override
    }
    protected abstract bool CanDo(); 
    protected virtual void FixedUpdate()
    {
        this.Action();
    }
}
