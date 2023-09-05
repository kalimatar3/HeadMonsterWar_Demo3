using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieHpbar : Hpbar
{
    protected virtual void LoadHp()
    {
        CurrentHp = this.DameReciver.GetComponent<EnemiesReciver>().CurrentHp;
        this.MaxHp = this.DameReciver.GetComponent<EnemiesReciver>().MaxHp;
    }
    protected override void ShowHp()
    {
        this.LoadHp();
        base.ShowHp();
    }
}
