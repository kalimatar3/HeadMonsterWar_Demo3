using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHeathButon : UpdateButton
{
    protected override void Actbutton()
    {
        if(DataManager.Instance.GetCost("Hp") > 0)
        {
            DataManager.Instance.IcrMaxHp();
            EffectSpawner.Instance.Spawn(CONSTEffect.UpGradeHeathEffect,PlayerController.Instance.transform.position,Quaternion.identity);
        }
        base.Actbutton();
    }
}
