using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReloadSlider : BaseSlider
{
    [SerializeField] protected Button ReloadButton;
    protected void peform()
    {
        this.Slider.value = PlayerController.Instance.GunCtrl.Shooting.reloadtimer/PlayerController.Instance.GunCtrl.Shooting.Reloadtime;
    }
    protected void FixedUpdate()
    {
        this.peform();
    }
    protected void OnEnable()
    {
        this.ReloadButton.enabled = false;
    }
    protected void OnDisable()
    {
        this.ReloadButton.enabled = true;
    }
}
