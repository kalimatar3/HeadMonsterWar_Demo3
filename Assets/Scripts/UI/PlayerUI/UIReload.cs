using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIReload : UIperform
{
    protected override void PreformText()
    {
        base.PreformText();
        this.Text.text = string.Format("{0:0.00}",PlayerController.Instance.GunCtrl.Shooting.reloadtimer);
    }
}
