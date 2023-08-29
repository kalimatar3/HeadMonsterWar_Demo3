using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBuyButton : BuyButton
{
    protected override void Actbutton()
    {
        DataManager.Instance.Unlock(ButtonStatus.Lock);
        foreach(RectTransform element in ButtonManager.Instance.ListSelectButton)
        {
            if(ButtonStatus.Select.name == element.name)
            {
                DataManager.Instance.CurrentGunName = ButtonStatus.Select.name;
            }
        }
        base.Actbutton();
        //Lsmanager.Instance.SaveGame();
    } 
}
