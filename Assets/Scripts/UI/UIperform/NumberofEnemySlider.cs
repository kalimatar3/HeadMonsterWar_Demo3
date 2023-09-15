 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NumberofEnemySlider : BaseSlider
{
    [SerializeField] protected Text thisText;
    protected float CurrentVelocity = 0;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }
    protected void LoadText()
    {
        thisText = GetComponentInChildren<Text>();
    }
    protected void  ShowText()
    {
        thisText.text = (LevelManager.Instance.NumberofPreCE + LevelManager.Instance.NumberofAliveCE).ToString()  + "/" + LevelManager.Instance.NumberofAllCE.ToString();
    }
    protected void ShowEnemiesSlider()
    {
        if(LevelManager.Instance.NumberofAllCE != 0)
        {
            float number =   ((float)LevelManager.Instance.NumberofPreCE + (float)LevelManager.Instance.NumberofAliveCE)/(float)LevelManager.Instance.NumberofAllCE;
            float Currentvalue = Mathf.SmoothDamp( Slider.value,number,ref CurrentVelocity, 50 * Time.deltaTime);
            Slider.value = Currentvalue;
        }
    }
    protected void FixedUpdate()
    {
        this.ShowText();
        this.ShowEnemiesSlider();
    }
}
