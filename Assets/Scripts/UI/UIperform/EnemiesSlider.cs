using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSlider : BaseSlider
{
    protected void ShowEnemiesSlider()
    {
        if(LevelManager.Instance.NumberofAllCE != 0)
        {
            float number =   (float)LevelManager.Instance.NumberofAliveCE/(float)LevelManager.Instance.NumberofAllCE;
            Slider.value = number;
        }
    }
    protected void FixedUpdate()
    {
        this.ShowEnemiesSlider();
    }
}
