using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelButtonStatus : ButtonStatus
{
    protected override void SlectMode()
    {
        if(Select.name != DataManager.Instance.CurrentModelName)
        {
            Selectbutton.gameObject.SetActive(true);            
        }
    } 
}
