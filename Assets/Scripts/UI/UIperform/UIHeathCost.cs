using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHeathCost : UIperform
{
    protected string mes;
   protected override void PreformText()
    {
        base.PreformText();
        if(DataManager.Instance.GetCost("Hp") <= 0)  mes = "MAX";
        else mes = DataManager.Instance.GetCost("Hp").ToString();
        Text.text = mes;
    }
}
