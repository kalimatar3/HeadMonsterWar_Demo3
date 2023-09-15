using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ButtonScale : MyBehaviour
{
    [SerializeField] protected Vector3 DefaultScale;
    protected override void Start()
    {
        base.Start();
        DefaultScale = this.transform.localScale;
    } 
    protected void OnEnable()
    {
        this.transform.DOScale(new Vector3(0.7f,0.7f,0.7f),0.5f)
        .SetLoops(-1,LoopType.Yoyo)
        .SetEase(Ease.Linear);
    }
    protected void OnDisable()
    {
        this.transform.DOScale(DefaultScale,0f)
        .SetLoops(-1,LoopType.Yoyo)
        .SetEase(Ease.Linear);
    }
}
