using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpHeathPerform : UIperform
{
    protected override void PreformText()
    {
        base.PreformText();
        Text.text = "Lv "  + DataManager.Instance.GetUpgradenumberfromUGAD("Hp").ToString();
    } 
}
