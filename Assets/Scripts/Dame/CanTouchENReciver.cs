using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CanTouchENReciver : DameReciver
{
    public bool Touched;
    protected override void Start()
    {
        base.Start();
        Touched = false;
        this.StartCoroutine(Dead());
    }
    protected override IEnumerator Dead()
    {
        yield return new WaitUntil(predicate:()=>
        {
           return Candead();
        });
        yield return new WaitForSeconds(3f);
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1f);
    }
    protected override bool Candead()
    {
        return Touched;
    } 
}
