using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCoinbutton : UpdateButton
{
    protected override void Actbutton()
    {
        if(DataManager.Instance.GetCost("Coin") > 0)
        { 
            DataManager.Instance.IcrCoinEarn();
            EffectSpawner.Instance.Spawn(CONSTEffect.UpGradeCoinEffect,PlayerController.Instance.transform.position,Quaternion.identity);
        }
        base.Actbutton();
    }
}
