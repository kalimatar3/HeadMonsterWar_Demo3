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
        }
        base.Actbutton();
    }
}
