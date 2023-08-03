using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider),typeof(Rigidbody))]
public class DameDealer : MyBehaviour
{
    [SerializeField] public float dealnumber;
    protected virtual void SendDametoObj(Transform obj)
    {
        Debug.Log(obj.transform.parent.ToString() + "is Taken dame by" + this.transform.parent.ToString());
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        this.SendDametoObj(other.transform);
    }
} 
