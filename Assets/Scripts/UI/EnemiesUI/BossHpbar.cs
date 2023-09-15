using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class BossHpbar : EnemieHpbar
{
    [SerializeField] protected Text BossName;
    [SerializeField] Transform Icon;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossName();
    }
    protected void LoadBossName()
    {
        BossName = this.transform.GetComponentInChildren<Text>();
    }
    protected override void LoadTextMesh()
    {
        // emty
    }
    protected override void ShowHp()
    {
        this.LoadHp();
        float HpPercent = CurrentHp/MaxHp;
        this.Slider.value = HpPercent;
        BossName.text = DameReciver.transform.parent.name;
        this.ShowIcon();
    }
    protected void ShowIcon()
    {
        foreach(Transform element in Icon)
        {
            element.gameObject.SetActive(false);
            if(element.name == BossName.text)
            {
                element.gameObject.SetActive(true);
            }
        }
    }
}
