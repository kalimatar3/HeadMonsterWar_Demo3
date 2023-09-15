using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopCost : UIperform
{
    protected override void PreformText()
    {
        base.PreformText();
        if(ButtonManager.Instance.Currentbutton ==null) return;
        foreach(DataManager.ShopData element in DataManager.Instance.ListShopData )
        {
            if(ButtonManager.Instance.Currentbutton.name == element.Name)
            {
                this.Text.text = element.Cost.ToString();
                // if(DataManager.Instance.Coin < element.Cost)
                // {
                //     this.Text.color =  Color.red;
                // }
                // else   this.Text.color =  Color.white;
            }
        }
    }
}
