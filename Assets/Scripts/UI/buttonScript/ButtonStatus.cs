using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStatus : MyBehaviour
{
    [SerializeField] public Transform Lock;
    [SerializeField] public Transform Select;
    [SerializeField] protected Transform Selectbutton,BuyButton;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadListButton();
    }
    protected void OnEnable()
    {
        StartCoroutine(this.Perform());
    }
    protected void LoadListButton()
    {
        foreach(Transform element in this.transform)
        {
            if(element.GetComponent<SelectButton>() != null) Selectbutton = element;
            if(element.GetComponent<BuyButton>()!= null) BuyButton = element;
        }
    }
    protected virtual void SlectMode()
    {
        //override
    }
    protected virtual void BuyMode()
    {
        foreach(DataManager.ShopData element in DataManager.Instance.ListShopData)
        {
            if(this.Lock.name == element.Name)
            {
               BuyButton.gameObject.SetActive(!element.Available);
               BuyButton.GetComponent<BuyButton>().ShowCantBuybutton();
            }
        }
    }
    protected virtual void SelectedMode()
    {
        if(Select.name == DataManager.Instance.CurrentMap||Select.name == DataManager.Instance.CurrentModelName || Select.name == DataManager.Instance.CurrentGunName)
        {
            Selectbutton.gameObject.SetActive(false);
            BuyButton.gameObject.SetActive(false);
        }
    }
    public virtual IEnumerator Perform()
    {
        yield return new WaitUntil(predicate:()=>
        {
            if(DataManager.Instance.CurrentMap == "") return false;
            return true;
        });
        yield return new WaitUntil(predicate:()=>
        {
            if(DataManager.Instance.ListShopData == null) return false;
            return true;
        });
        Selectbutton.gameObject.SetActive(false);
        BuyButton.gameObject.SetActive(false);
        this.BuyMode();
        this.SlectMode();
        this.SelectedMode();
    }
}
