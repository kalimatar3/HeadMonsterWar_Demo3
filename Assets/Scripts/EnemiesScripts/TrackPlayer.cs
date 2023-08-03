using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrackPlayer : MyBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float stopDis;
    [SerializeField] protected NavMeshAgent thisNav; 
    public bool Tracking;
    [SerializeField] protected EnemieCtrl EnemieCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadthisNav();
        this.LoadEnemyCTrl();
    }
    protected void LoadEnemyCTrl()
    {
        EnemieCtrl = GetComponentInParent<EnemieCtrl>();
    }
    protected void LoadthisNav()
    {
        thisNav = GetComponentInParent<NavMeshAgent>();
        if(thisNav == null) return;
    }
    protected virtual void Track()
    {
        Vector3 Direction = (PlayerController.Instance.transform.position - this.transform.parent.position).normalized;
        float Distance =  (PlayerController.Instance.transform.position - this.transform.parent.position).magnitude;
        if(Distance > stopDis && !EnemieCtrl.EnemieAct.gate) 
        {
            Tracking = true;
            if(thisNav != null)
            thisNav.SetDestination(PlayerController.Instance.transform.position);
            thisNav.speed = speed;
        }
        if(Distance <= stopDis && EnemieCtrl.EnemieAct.gate)
        {
            thisNav.SetDestination(this.transform.parent.position);
             if(Vector3.Angle(Direction,this.transform.parent.forward) >= 90 || Vector3.Angle(Direction,this.transform.parent.forward) <= -90)
             this.transform.parent.forward = Vector3.Lerp(this.transform.parent.forward,Direction,Time.deltaTime * 1f * 4);
            Tracking = false;
        }
    }
    protected void FixedUpdate()
    {
        this.Track();
    }
}
