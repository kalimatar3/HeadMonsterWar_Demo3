using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialPointMoveVer : MyBehaviour
{
    protected override void Start()
    {
        this.transform.DOLocalMoveY(25f,0.5f)
        .SetLoops(-1,LoopType.Yoyo)
        .SetRelative()
        .SetEase(Ease.Linear);
    }
}
