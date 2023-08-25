using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapBuyButton : ButtonBase
{
    [SerializeField] protected MapButtonStatus mapButtonStatus;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMapbuttonstatus();
    } 
    protected void LoadMapbuttonstatus()
    {
        mapButtonStatus = GetComponentInParent<MapButtonStatus>();
        if(mapButtonStatus == null) Debug.LogWarning(this.transform + "dont have Mapbuttonstatus");
    } 
    protected override void Actbutton()
    {
        base.Actbutton();
        DataManager.Instance.Unlock(mapButtonStatus.Lock);
        foreach(RectTransform element in ButtonManager.Instance.SelectButton)
        {
            if(mapButtonStatus.ThisMap.name == element.name)
            {
                DataManager.Instance.CurrentMap = mapButtonStatus.ThisMap.name;
            }
        }
        Lsmanager.Instance.SaveGame();
        ScenesManager.Instance.LoadScene(SceneManager.GetActiveScene().name); 
    }  
}
