using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectbutton : ButtonBase
{
    protected override void Actbutton()
    {
        this.Load();
        ModelManager.Instance.ActiveModel();
        GunCtrl.Instance.ActiveGun();
        base.Actbutton();
    }
    protected IEnumerator DelayLoad()
    {
        yield  return new WaitUntil(predicate : ()=>
        {
            if(ButtonManager.Instance.Currentbutton == null) return false;
            return true;
        });
    }
    protected void Load()
    {
        foreach(Transform element in ModelManager.Instance.ListModel)
        {
            if(element.name == ButtonManager.Instance.Currentbutton.name)
            {
                DataManager.Instance.CurrentModelName = element.name;
            }
        }
        foreach(Transform element in GunCtrl.Instance.ListGuns)
        {
            if(element.name == ButtonManager.Instance.Currentbutton.name)
            {
                DataManager.Instance.CurrentGunName = element.name;
            }

        }
    }
}
