using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuntipScript : MyBehaviour
{
    [SerializeField] protected EnemieCtrl enemieCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    } 
    protected void LoadEnemyCtrl()
    {
        this.enemieCtrl = transform.parent.GetComponentInParent<EnemieCtrl>();
    }
    [SerializeField] protected Transform Target;
    protected void FixedUpdate()
    {
        if(!enemieCtrl.TrackPlayer.Tracking)
        {
            Vector3 huongsung = Target.transform.position - transform.position;
            this.transform.forward = huongsung.normalized;
            this.Target.forward = -huongsung.normalized;
        }
        else
        {
            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = new Quaternion(0,0,0,0);
        }
    }
}
