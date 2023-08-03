using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinlevelReward : MyBehaviour
{
    [SerializeField] protected float NumberofCoins;
    protected void OnEnable()
    {
        DataManager.Instance.IcrGold((int)NumberofCoins);
    }
}

