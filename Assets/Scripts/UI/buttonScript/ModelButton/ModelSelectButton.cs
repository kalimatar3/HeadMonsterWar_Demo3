using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSelectButton : SelectButton
{
    protected override void Actbutton()
    {
        foreach(RectTransform element in ButtonManager.Instance.ListSelectButton)
        {
            if(ButtonStatus.Select.name == element.name)
            {
                DataManager.Instance.CurrentModelName = ButtonStatus.Select.name;
            }
        }
        base.Actbutton();
        //Lsmanager.Instance.SaveGame();
    } 
}
