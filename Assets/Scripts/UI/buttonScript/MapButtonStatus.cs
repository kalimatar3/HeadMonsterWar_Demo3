using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButtonStatus : MyBehaviour
{
    [SerializeField] public Transform Lock;
    [SerializeField] public Transform ThisMap;
    protected void OnEnable()
    {
        StartCoroutine(this.ButtonStatus());
    }
    protected void SlectMode()
    {
        if(ThisMap.name != DataManager.Instance.CurrentMap)
        {
            this.transform.GetChild(1).gameObject.SetActive(true);            
        }
    }
    protected void BuyMode()
    {
        foreach(DataManager.ShopData element in DataManager.Instance.ListShopData)
        {
            if(this.Lock.name == element.Name)
            {
                this.transform.GetChild(2).gameObject.SetActive(!element.Available);
            }
        }
    }
    protected void SelectedMode()
    {
        if(ThisMap.name == DataManager.Instance.CurrentMap)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(2).gameObject.SetActive(false);
        }

    }
    public IEnumerator ButtonStatus()
    {
        yield return new WaitUntil(predicate:()=>
        {
            if(DataManager.Instance.ListShopData == null) return false;
            return true;
        });
        yield return new WaitUntil(predicate:()=>
        {
            if(DataManager.Instance.CurrentMap == "") return false;
            return true;
        });

        foreach(Transform element in this.transform)
        {
            element.gameObject.SetActive(false);
        }
        this.BuyMode();
        this.SlectMode();
        this.SelectedMode();
    }
}
