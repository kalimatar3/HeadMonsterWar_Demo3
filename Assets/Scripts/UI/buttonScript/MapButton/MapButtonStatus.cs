using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButtonStatus : ButtonStatus
{    
    protected override void SlectMode()
    {
        if(Select.name != DataManager.Instance.CurrentMap)
        {
           Selectbutton.gameObject.SetActive(true);            
        }
    } 
}
