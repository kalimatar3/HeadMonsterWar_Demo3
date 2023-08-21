using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class waveTagPerform : MyBehaviour
{
    [SerializeField] protected Text Thistext;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }
    protected void OnEnable()
    {
        this.StartCoroutine(this.Appear());
    }
    protected void LoadText()
    {
        Thistext = GetComponentInChildren<Text>();
    }
    protected void PerformStartWaveText(String WaveName)
    {
        string Mes = WaveName + " Started";
        Thistext.text = Mes;
    }
    protected void PerformEndWaveText(String WaveName)
    {
        string Mes = WaveName + " Survived";
        Thistext.text = Mes;
    }
    protected IEnumerator Appear()
    {
        yield return new WaitUntil(predicate:()=>
        {
            if(this.transform.localScale.y > 0) return false;
            return true;
        });
        this.transform.DOScaleY(1,0.2f);
        yield return new WaitUntil(predicate:()=>
        {
            if(this.transform.localScale.y < 1) return false;
            return true;
        });
        yield return new WaitForSeconds(1f);
        this.transform.DOScaleY(0,0.2f);
        yield return new WaitUntil(predicate:()=>
        {
            if(this.transform.localScale.y > 0 ) return false;
            return true;
        });
        this.transform.gameObject.SetActive(false);
    }   
    public IEnumerator StartWavePerform(String WaveName)
    {
        yield return new WaitUntil(predicate:()=>
        {
            if(LevelManager.Instance.CrLevelname == "") return false;
            return true;
        });
        PerformStartWaveText(WaveName);
    }
    public IEnumerator ENdWavePerform(String WaveName)
    {
        yield return new WaitUntil(predicate:()=>
        {
            if(LevelManager.Instance.CrLevelname == "") return false;
            return true;
        });
       this.PerformEndWaveText(WaveName);
    }
}
