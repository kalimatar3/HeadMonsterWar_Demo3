using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICoinCost : UIperform
{
    protected string mes;
   protected override void PreformText()
    {
        base.PreformText();
        if(DataManager.Instance.GetCost("Coin") <= 0)  mes = "MAX";
        else 
        {
            mes = DataManager.Instance.GetCost("Coin").ToString();
        }
        Text.text = mes;
    }
}
