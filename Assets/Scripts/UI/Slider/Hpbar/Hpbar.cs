using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Hpbar : BaseSlider
{
    [SerializeField] public Transform DameReciver;
    [SerializeField] public float CurrentHp,MaxHp;
    protected TextMeshPro Hpnumber;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTextMesh();
    }
    protected virtual void LoadTextMesh()
    {
        TextMeshPro Hpnumber = this.transform.GetComponentInChildren<TextMeshPro>();
        if(Hpnumber == null) Debug.LogWarning(this.transform + "Can't Found TextMesh");
        this.Hpnumber = Hpnumber;
    }
    protected virtual void ShowHp()
    {
        float HpPercent = CurrentHp/MaxHp;
        this.Hpnumber.SetText(((int)CurrentHp).ToString());
        this.Slider.value = HpPercent;
    }
    protected void FixedUpdate()
    {
        this.ShowHp();
    }
}
