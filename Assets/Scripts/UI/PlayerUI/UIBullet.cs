using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBullet : TextMeshPerform
{
    protected override void PreformText()
    {
        base.PreformText();
        this.Text.text = (PlayerController.Instance.GunCtrl.Shooting.CurrentAmmo.ToString() + "/" + PlayerController.Instance.GunCtrl.Shooting.MaxAmmo);
    }
}
