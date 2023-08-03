using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuntipScript : MyBehaviour
{
    [SerializeField] protected Transform Target;
    protected void FixedUpdate()
    {
        Vector3 huongsung = Target.transform.position - transform.position;
        this.transform.forward = huongsung.normalized;
        this.Target.forward = -huongsung.normalized;
    }
}
