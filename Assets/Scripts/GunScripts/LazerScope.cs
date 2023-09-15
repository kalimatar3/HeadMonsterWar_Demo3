using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScope : MyBehaviour
{
    [SerializeField] protected LineRenderer line;
    [SerializeField] protected Transform Guntip;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLine();
    } 
    protected void LoadLine()
    {
        line = GetComponent<LineRenderer>();
    }
    protected void Scoping()
    {
        Vector3 Tip = new Vector3(InputManager.Instance.Shootingstick.Horizontal,0,InputManager.Instance.Shootingstick.Vertical);
        if(Tip.magnitude > 0)
        {
            line.positionCount = 2;
            line.SetPosition(0,Guntip.position);
            line.SetPosition(1,Guntip.position + new Vector3(Guntip.position.x- PlayerController.Instance.transform.position.x,0, Guntip.position.z- PlayerController.Instance.transform.position.z) * 20);
        }
        else
        {
            line.positionCount = 0;
        }
    }
    protected void FixedUpdate()
    {
        this.Scoping();
    }
}
