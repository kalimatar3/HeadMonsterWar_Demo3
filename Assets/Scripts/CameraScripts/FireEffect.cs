using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MyBehaviour
{
    [SerializeField] protected Vector3 Firecampos;
    [SerializeField]protected float smooth;
    protected virtual void fireEffect()
    {
        if(InputManager.Instance.Shootingstick.Horizontal !=0 ||InputManager.Instance.Shootingstick.Vertical != 0)
        {
            Vector3 NewPOs =  Vector3.Lerp(this.transform.parent.position,Firecampos,smooth);
            this.transform.parent.position = NewPOs;
        }
    }
    protected void FixedUpdate()
    {
        this.fireEffect();
    }
}
