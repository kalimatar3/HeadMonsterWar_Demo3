using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PointEffect : MyBehaviour
{
    [SerializeField] Vector3 BasePOs;
    protected override void Start()
    {
        base.Start();
        BasePOs = this.transform.position; 
    }
    protected void OnEnable()
    {
        this.transform.DOMoveY(BasePOs.x + 40f,1f).SetLoops(-1,LoopType.Yoyo)
        .SetRelative()
        .SetEase(Ease.Linear);
    }
}
