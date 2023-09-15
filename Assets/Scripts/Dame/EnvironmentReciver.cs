using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentReciver : DameReciver
{
    public bool Camblock;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.MaxHp = 100000000;
    }
}
