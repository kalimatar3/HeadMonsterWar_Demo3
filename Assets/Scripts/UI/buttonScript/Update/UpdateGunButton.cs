using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGunButton : UpdateButton
{
    protected override void Actbutton()
    {
        if(DataManager.Instance.GetCost(DataManager.Instance.CurrentGunName) > 0)
        {
            DataManager.Instance.IcrMaxbullet();
            EffectSpawner.Instance.Spawn(CONSTEffect.UpGradeAmmoEffect,PlayerController.Instance.transform.position,Quaternion.identity);
        }
        base.Actbutton();
    }
}
