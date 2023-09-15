using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ButtonRotate : MyBehaviour
{
    protected void OnEnable()
    {
        this.transform.DORotate(new Vector3(0,0,360),2f)
        .SetRelative()
        .SetEase(Ease.Linear);
    }
}
