using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaxbulletCost : UIperform
{
    protected string mes;
   protected override void PreformText()
    {
        base.PreformText();
        if(DataManager.Instance.GetCost(DataManager.UpgradeabledataName.IcreMaxbulletPistolCost.ToString()) <= 0)  mes = "MAX";
        else mes = DataManager.Instance.GetCost(DataManager.UpgradeabledataName.IcreMaxbulletPistolCost.ToString()).ToString();
        Text.text = mes;
    }
}
