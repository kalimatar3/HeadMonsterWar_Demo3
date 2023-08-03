using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHpbar : Hpbar
{
    protected static PlayerHpbar instance;
    public static PlayerHpbar Instance { get => instance ;}
     public float PlayerCurrentHP;
   protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
    protected virtual void LoadHp()
    {
        PlayerCurrentHP = CurrentHp;
        this.CurrentHp = this.DameReciver.GetComponent<PlayerReciver>().CurrentHp;
        this.MaxHp = this.DameReciver.GetComponent<PlayerReciver>().MaxHp;
    }
    protected override void ShowHp()
    {
        this.LoadHp();
        base.ShowHp();
    }
}
