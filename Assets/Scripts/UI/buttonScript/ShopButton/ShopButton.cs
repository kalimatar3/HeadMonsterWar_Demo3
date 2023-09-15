using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShopButton : ButtonBase
{
    [SerializeField] protected RectTransform ThisRecttrans;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadThisRectTrans();
    }
    protected void LoadThisRectTrans()
    {
        ThisRecttrans = GetComponent<RectTransform>();
        if(ThisRecttrans == null) Debug.Log(this.transform + "dont Have RectTrans");
    }
    protected override void Actbutton()
    {
        this.GetCurrentButton();
        base.Actbutton();
    }
    protected void GetCurrentButton()
    {
        this.Buttonsound();
        if(!this.transform.gameObject.activeInHierarchy)
        {
            ButtonManager.Instance.Currentbutton = null;
            return;
        }
            ButtonManager.Instance.Currentbutton = ThisRecttrans;
    }
}
