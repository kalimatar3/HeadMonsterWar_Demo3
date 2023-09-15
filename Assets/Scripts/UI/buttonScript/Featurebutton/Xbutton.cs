using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xbutton : ButtonBase
{
    protected override void Actbutton()
    {
        PanelCtrl.Instance.HirePanel(this.transform.parent.name);
        PanelCtrl.Instance.ShowPanel("MainMenuPannel");
        base.Actbutton();
    }
}
