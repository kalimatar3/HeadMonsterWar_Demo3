using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonBase : MyBehaviour
{
    [SerializeField] protected Button thisbutton;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadthisbutton();
        this.AddActButton();
    }
    protected void Loadthisbutton()
    {
        thisbutton = GetComponent<Button>();
        if(thisbutton == null) Debug.LogWarning( this.transform + "dont have button");
    }
    protected void AddActButton()
    {
        if(thisbutton == null) return;
        thisbutton.onClick.AddListener(delegate () {this.Actbutton();});
    }
    protected virtual void Actbutton()
    {
        this.Buttonsound();
        Lsmanager.Instance.SaveGame();
    }
    protected void Buttonsound()
    {
        SoundSpawner.Instance.Spawn(CONSTSoundsName.ButtonTap,Vector3.zero,Quaternion.identity);
    }
    protected void  GetCurrentButton(Transform  obj)
    {
        ButtonManager.Instance.Currentbutton = obj.GetComponent<RectTransform>();
    }
}
