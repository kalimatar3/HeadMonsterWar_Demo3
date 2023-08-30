using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadSlider : BaseSlider
{
    protected void peform()
    {
        this.Slider.value = PlayerController.Instance.GunCtrl.Shooting.reloadtimer/PlayerController.Instance.GunCtrl.Shooting.Reloadtime;
    }
    protected void FixedUpdate()
    {
        this.peform();
    }
}
