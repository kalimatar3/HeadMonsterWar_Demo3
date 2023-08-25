using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICoin : MyBehaviour
{
    protected Text Gold;
    [SerializeField] protected int coinnumber;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCoins();
    }
    protected virtual void LoadCoins()
    {
        if(Gold != null) return;
        Gold = GetComponent<Text>();
    }
    protected void showcoins()
    {
        this.Gold.text = (DataManager.Instance.Coin.ToString());
    }
    protected void FixedUpdate()
    {
        this.showcoins();
    }
}
