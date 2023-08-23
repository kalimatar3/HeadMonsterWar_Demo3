using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPointFollow : followObj
{
    [SerializeField] protected float Radius;
    protected override void follow()
    {
        Vector3 Dir = Obj.transform.position - PlayerController.Instance.transform.position;
        if(Dir.magnitude <= Radius)
        {
            this.transform.parent.position = Obj.transform.position +  new Vector3(0,5,0);
            this.transform.localPosition =  new Vector3(0,0,0);
            this.transform.GetChild(0).transform.localPosition = new Vector3(0,0,2);
            this.transform.forward = Vector3.down;
        }
        else
        {
            this.transform.localPosition =  new Vector3(0,5,0);
            this.transform.GetChild(0).transform.localPosition = new Vector3(0,-5,2);
            this.transform.parent.position = PlayerController.Instance.transform.position + Dir.normalized * (Radius - 2);
            this.transform.forward = Dir.normalized; 
        }
    }
}
