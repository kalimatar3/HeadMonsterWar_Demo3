using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHeathButon : UpdateButton
{
    protected override void Actbutton()
    {
        DataManager.Instance.IcrMaxHp();
        base.Actbutton();
    }
}
