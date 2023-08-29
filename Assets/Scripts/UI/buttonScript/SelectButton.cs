using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectButton : ButtonBase
{
    [SerializeField] protected ButtonStatus ButtonStatus;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMapbuttonstatus();
    } 
    protected void LoadMapbuttonstatus()
    {
        ButtonStatus = this.transform.GetComponentInParent<ButtonStatus>();
        if(ButtonStatus == null) Debug.LogWarning(this.transform + "dont have Mapbuttonstatus");
    } 
    protected override void Actbutton()
    {
        base.Actbutton();
        ModelManager.Instance.ActiveModel();
        GunCtrl.Instance.ActiveGun();
        StartCoroutine(ButtonStatus.Perform());
    } 
}
