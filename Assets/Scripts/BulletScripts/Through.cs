using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Through : MyBehaviour
{
    [SerializeField] protected float speed;
    protected void fly()
    {
        this.transform.parent.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    protected void FixedUpdate()
    {
        this.fly();
    }
}
