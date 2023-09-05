using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ButtonScale : MyBehaviour
{
    protected void OnEnable()
    {
        this.transform.DOScale(new Vector3(0.1f,0.1f,0.1f),0.5f)
        .SetLoops(-1,LoopType.Yoyo)
        .SetRelative()
        .SetEase(Ease.Linear);
    }
}
