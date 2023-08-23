using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPbarFollow : ForwardCamera
{
    protected override void follow()
    {
        Vector3 Direction = (this.Obj.position - this.transform.parent.position).normalized;
        Vector3 Dir = new Vector3(0,Direction.y,Direction.z);
        this.transform.parent.forward = -Dir;
    }
}
