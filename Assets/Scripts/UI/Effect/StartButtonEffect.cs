using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartButtonEffect : MyBehaviour
{
    protected void OnEnable()
    {
        this.transform.DOScale(new Vector3(0.2f,0.2f,0.2f),1f).SetLoops(-1,LoopType.Yoyo)
        .SetRelative()
        .SetEase(Ease.Linear);
    }
}
