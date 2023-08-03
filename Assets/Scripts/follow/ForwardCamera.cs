using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardCamera : followObj
{
    protected override void follow()
    {
        Vector3 Direction = (this.Obj.position - this.transform.parent.position).normalized;
        this.transform.parent.forward = -Direction;
    }
}
