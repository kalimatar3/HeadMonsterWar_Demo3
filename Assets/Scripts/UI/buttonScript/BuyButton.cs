using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuyButton : ButtonBase
{
    [SerializeField] protected Transform CantBuybutton;
    [SerializeField] protected ButtonStatus ButtonStatus;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMapbuttonstatus();
    } 
    protected void LoadMapbuttonstatus()
    {
        ButtonStatus = GetComponentInParent<ButtonStatus>();
        if(ButtonStatus == null) Debug.LogWarning(this.transform + "dont have Mapbuttonstatus");
    }
    public void ShowCantBuybutton()
    {
        foreach(DataManager.ShopData element in DataManager.Instance.ListShopData)
        {
            if(ButtonStatus.Lock.name == element.Name)
            {
                if(DataManager.Instance.Coin < element.Cost) CantBuybutton.gameObject.SetActive(true);
                else CantBuybutton.gameObject.SetActive(false);
            }
        }
    }
    protected override void Actbutton()
    {
        base.Actbutton();
        ModelManager.Instance.ActiveModel();
        GunCtrl.Instance.ActiveGun();
        StartCoroutine(ButtonStatus.Perform());
    }  
}
