using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MapSelectButton : SelectButton
{
    protected override void Actbutton()
    {
        foreach(RectTransform element in ButtonManager.Instance.ListSelectButton)
        {
            if(ButtonStatus.Select.name == element.name)
            {
                DataManager.Instance.CurrentMap = ButtonStatus.Select.name;
            }
        }
        base.Actbutton();
        //Lsmanager.Instance.SaveGame();
        ScenesManager.Instance.LoadScene(SceneManager.GetActiveScene().name); 
        this.StartCoroutine(MapManager.Instance.DelayLoadMap());
    } 
}
