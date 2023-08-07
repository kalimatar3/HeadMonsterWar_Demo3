using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : ShopButton
{
    protected override void Actbutton()
    {
        base.Actbutton();
    }
    protected void Buy()
    {
        if(ButtonManager.Instance.Currentbutton == null) return;
        if(ButtonManager.Instance.Currentbutton.parent != null) return;
    }
}
