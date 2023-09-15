using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPoint : followObj
{
    [SerializeField] protected float Radius;
    [SerializeField] protected Transform Model;
    protected Vector3 Dir;
    protected override void follow()
    {
        Vector3 OriPOs = PlayerController.Instance.transform.position +  new Vector3(0,0,3);
        Dir = Obj.transform.position - OriPOs;
        Vector3 Direction = Obj.transform.position -  PlayerController.Instance.transform.position; 
        if( Mathf.Abs(Dir.x) >= Radius*1920f/960 || Mathf.Abs(Dir.z) >= Radius)
        {
            Model.gameObject.SetActive(true);
            this.transform.localPosition =  new Vector3(0,5,0);
            this.transform.GetChild(0).transform.localPosition = new Vector3(0,-5,2);
            this.transform.parent.position = new Vector3(PlayerController.Instance.transform.position.x + Direction.normalized.x * 4 * 1920/960,0,PlayerController.Instance.transform.position.z + Direction.normalized.z * 4);
            this.transform.forward = Direction.normalized; 
        }
        else
        {
          Model.gameObject.SetActive(false);  
        }
    }
    }
