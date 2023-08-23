using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpCoinEarnPerform : UIperform
{
    protected override void PreformText()
    {
        base.PreformText();
        Text.text = "Lv"  + DataManager.Instance.GetUpgradenumberfromUGAD("Coin").ToString();
    } 
}
