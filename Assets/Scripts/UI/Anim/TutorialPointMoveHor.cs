using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialPointMoveHor : MyBehaviour
{
    protected override void Start()
    {
        this.transform.DOLocalMoveX(50f,0.5f)
        .SetLoops(-1,LoopType.Yoyo)
        .SetRelative()
        .SetEase(Ease.Linear);
    }
}
