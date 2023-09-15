using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MyBehaviour
{
    protected void OnEnable()
    {
        this.transform.rotation =  new Quaternion(0,150,0,0);
    }
    [SerializeField] protected float speed;
    protected void autorotate()
    {
        this.transform.Rotate(Vector3.up, Time.deltaTime * 1f * speed);
    }
    protected void FixedUpdate()
    {
        this.autorotate();
    }
}
