using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DropItemAnim : MyBehaviour
{
    [SerializeField] protected Vector3 BaseloPos;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.BaseloPos = this.transform.localPosition;
    }
    protected void OnEnable()
    {
        Tweener tweener =  this.transform.DORotate(new Vector3(0,360,0),3f,RotateMode.FastBeyond360)
        .SetLoops(-1,LoopType.Restart)
        .SetRelative()
        .SetEase(Ease.Linear);
        this.transform.DOLocalMoveY(0.1f,3f)            
        .SetLoops(-1,LoopType.Yoyo)
        .SetRelative()
        .SetEase(Ease.Linear);
    }
}
