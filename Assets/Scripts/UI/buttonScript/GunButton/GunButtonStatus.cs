using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunButtonStatus : ButtonStatus
{
    protected override void SlectMode()
    {
        if(Select.name != DataManager.Instance.CurrentGunName)
        {
            Selectbutton.gameObject.SetActive(true);            
        }
    } 
}
