using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockButton : ButtonBase
{
    protected override void Actbutton()
    {
        ButtonManager.Instance.LoadPlayerModel(this.transform.parent);
        this.GetCurrentButton(this.transform);
        base.Buttonsound();
    }
}
