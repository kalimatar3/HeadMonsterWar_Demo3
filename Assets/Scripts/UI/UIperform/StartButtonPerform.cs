using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StartButtonPerform : MyBehaviour
{
    protected void OnEnable()
    {
        this.transform.localScale = new Vector3(1.1f,0,1.1f);
        StartCoroutine(Appear());   
    }
    protected IEnumerator Appear()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(predicate:()=>
        {
            if(this.transform.localScale.y > 0) return false;
            return true;
        });
        this.transform.DOScaleY(1,0.2f);
    }   
}
