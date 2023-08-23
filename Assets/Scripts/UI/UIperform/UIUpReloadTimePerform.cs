using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpReloadTimePerform : UIperform
{
    protected override void PreformText()
    {
        base.PreformText();
        Text.text = "Lv"  + DataManager.Instance.GetUpgradenumberfromUGAD(DataManager.Instance.CurrentGunName).ToString();
    } 
}
