using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosstTrackPlayer : TrackPlayer
{
    [SerializeField] float test;
    protected override void Track()
    {
        Vector3 Direction = (PlayerController.Instance.transform.position - this.transform.parent.position).normalized;
        float Distance =  (PlayerController.Instance.transform.position - this.transform.parent.position).magnitude;
            test = Vector3.Angle(Direction,this.transform.parent.forward);
        if(Distance > stopDis && !EnemieCtrl.EnemieAct.gate)
        {
            Tracking = true;
            thisNav.SetDestination(PlayerController.Instance.transform.position);
            thisNav.speed = speed;
        }
        if(Distance <= stopDis )
        {
            thisNav.SetDestination(this.transform.parent.position);
            if(Vector3.Angle(Direction,this.transform.parent.forward) >= 120 || Vector3.Angle(Direction,this.transform.parent.forward) <= -90 )
            this.transform.parent.forward = Vector3.Lerp(this.transform.parent.forward,Direction,Time.deltaTime * 1f  * 3f);
            Tracking = false;
        }
    }
}
