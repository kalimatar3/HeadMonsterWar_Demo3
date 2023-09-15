using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllfollowObj : followObj
{
    protected override void follow()
    {
        base.follow();
        this.transform.rotation = Obj.transform.rotation;
    }
}
