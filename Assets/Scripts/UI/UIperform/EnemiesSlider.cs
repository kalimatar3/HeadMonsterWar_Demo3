 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesSlider : BaseSlider
{
    [SerializeField] protected Text thisText;
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
            Slider.value = number;
        }
    }
    protected void FixedUpdate()
    {
        this.ShowText();
        this.ShowEnemiesSlider();
    }
}
