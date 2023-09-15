using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateButton : ButtonBase
{
    protected override void Actbutton()
    {
        this.Reborn();
        base.Actbutton();
    }
    public void Reborn()
    {
        GunCtrl.Instance.Shooting.reborn();
        PlayerController.Instance.PlayerReciver.ReBorn();
        HolderManager.Instance.ActiveHolder("SoundHolder");
    }
}
