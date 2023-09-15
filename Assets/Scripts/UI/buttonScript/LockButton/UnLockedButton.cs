using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnLockedButton : ButtonBase
{
    protected override void Actbutton()
    {
        ButtonManager.Instance.LoadPlayerModel(this.transform);
        this.GetCurrentButton(this.transform);
        base.Buttonsound();
    }
}
