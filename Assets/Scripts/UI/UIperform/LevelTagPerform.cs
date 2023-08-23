using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelTagPerform : MyBehaviour
{
    protected void OnEnable()
    {
        this.StartCoroutine(this.Appear());
    }
    protected IEnumerator Appear()
    {
        yield return new WaitUntil(predicate:()=>
        {
            if(this.transform.localScale.y > 0) return false;
            return true;
        });
        this.transform.DOScaleY(1,0.2f);
    }
    protected void OnDisable()
    {
        this.transform.localScale = new Vector3(1,0,1);
    }    
}
