using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChanceWepon : MyBehaviour
{
    [SerializeField] protected List<Transform> ListUIGun;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadListUI();
    } 
    protected void LoadListUI()
    {
        if(ListUIGun.Count > 0 ) return;
        foreach(Transform element in this.transform)
        {
            ListUIGun.Add(element);
        } 
    }
    protected void OnEnable()
    {
        StartCoroutine(this.ShowCurrentWp());
    }
    protected IEnumerator ShowCurrentWp()
    {
        yield return new WaitUntil(predicate:()=>
        {
            if(DataManager.Instance.CurrentGunName == "") return false;
            return true;
        });
        yield return new WaitUntil(predicate:()=>
        {
            if(PlayerController.Instance.GunCtrl == null) return false;
            return true;
        });
        for(int i = 0 ; i < PlayerController.Instance.GunCtrl.ListGuns.Count; i++)
        {
             ListUIGun[i].gameObject.SetActive(false);
            if(PlayerController.Instance.GunCtrl.ListGuns[i].name == DataManager.Instance.CurrentGunName)
            {
                ListUIGun[i].gameObject.SetActive(true);
            }
        }
    }
}
