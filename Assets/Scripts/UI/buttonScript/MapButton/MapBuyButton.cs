using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapBuyButton : BuyButton
{
    protected override void Actbutton()
    {
        DataManager.Instance.Unlock(ButtonStatus.Lock);
        foreach(RectTransform element in ButtonManager.Instance.ListSelectButton)
        {
            if(ButtonStatus.Select.name == element.name)
            {
                DataManager.Instance.CurrentMap = ButtonStatus.Select.name;
            }
        }
        base.Actbutton();
        //Lsmanager.Instance.SaveGame();
        StartCoroutine(ScenesManager.Instance.LoadScene(SceneManager.GetActiveScene().name));
        this.StartCoroutine(MapManager.Instance.DelayLoadMap());
    }  
}
