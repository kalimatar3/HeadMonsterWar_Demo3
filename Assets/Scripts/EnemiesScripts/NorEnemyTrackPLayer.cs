using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorEnemyTrackPLayer : TrackPlayer
{
    [SerializeField] float BaseSpeed,OutRangeSpeed;
    protected override void Track()
    {
        float Distance =  (PlayerController.Instance.transform.position - this.transform.parent.position).magnitude;
        if(Distance > 10f) speed = OutRangeSpeed;
        else speed = BaseSpeed;
        base.Track();
    }
}
