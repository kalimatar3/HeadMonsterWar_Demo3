using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLazerTrackPlayer : BosstTrackPlayer
{
    [SerializeField] protected Transform TarGet;
    protected override void Track()
    {
        Vector3 Direction = (PlayerController.Instance.transform.position - this.transform.parent.position).normalized;
        float Distance =  (PlayerController.Instance.transform.position - this.transform.parent.position).magnitude;
        if(Distance > stopDis && !EnemieCtrl.EnemieAct.gate)
        {
            Tracking = true;
            thisNav.SetDestination(PlayerController.Instance.transform.position);
            thisNav.speed = speed;
        }
        if(Distance <= stopDis )
        {
            thisNav.SetDestination(this.transform.parent.position);
            if(Vector3.Angle(Direction,this.transform.parent.forward) >= 120)
            {
                followObj  targetfollow = TarGet.GetComponentInChildren<followObj>();
                targetfollow.smooth = 0f;
                this.transform.parent.forward = Vector3.Lerp(this.transform.parent.forward,Direction,Time.deltaTime * 1f *  1.5f);
            }
            else
            {
                followObj  targetfollow = TarGet.GetComponentInChildren<followObj>();
                targetfollow.smooth = 0.05f;  
            }
            Tracking = false;
        }
    }

}
