using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCoinbutton : UpdateButton
{
    protected override void Actbutton()
    {
        DataManager.Instance.IcrCoinEarn();
        base.Actbutton();
    }
}
