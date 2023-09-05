using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followObj : MyBehaviour
{
    [SerializeField] public float smooth;
    public Transform Obj;
    protected virtual void follow()
    {
        if(Obj == null) return;
        Vector3 newPos = Vector3.Lerp(this.transform.parent.position, Obj.transform.position,this.smooth);
        this.transform.parent.position = newPos;
    }
    protected virtual void FixedUpdate()
    {
        this.follow();
    }
}
