using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcusCamera : MyBehaviour
{
    [SerializeField] CameraFollow cameraFollow;
    protected override void Start()
    {
        base.Start();
        cameraFollow.Forcus(this.transform.parent,3f);
    }
}
