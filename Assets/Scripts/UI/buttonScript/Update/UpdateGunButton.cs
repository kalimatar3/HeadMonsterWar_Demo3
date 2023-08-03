using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGunButton : UpdateButton
{
    protected override void Actbutton()
    {
        DataManager.Instance.IcrMaxbullet();
        base.Actbutton();
    }
}
