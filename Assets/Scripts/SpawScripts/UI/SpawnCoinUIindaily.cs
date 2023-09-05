using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoinUIindaily : SpawnCoinsUI
{
    public void ClaimReward()
    {
        this.StartCoroutine(this.spawncoinsDelay());
    }
    protected override void OnEnable()
    {
        this.ShowCoinText();
    }
    public override IEnumerator spawncoinsDelay()
    {
        this.CurrentCoinTextNumber = 0;
        yield return new WaitUntil(predicate : ()=>
        {
            if(CoinUISpawner.Instance == null) return false;
            return true;
        }); 
        int sum = 0;
        while(sum < 10)
        {
            SoundSpawner.Instance.Spawn(CONSTSoundsName.PickCoin,this.transform.position,Quaternion.identity);
            Transform Coin =  CoinUISpawner.Instance.Spawn("Coin",this.CoinStart.position,Quaternion.identity);
            CoinFly thisCoinfly = Coin.GetComponentInChildren<CoinFly>();
            thisCoinfly.SpawnPos = this.CoinStart.position;
            thisCoinfly.StartPos = RandomPosAroundObj(CoinStart,new Vector2(1,2)); 
            thisCoinfly.EndPos = CoinEnd.position;
            sum ++;
        }
        yield return new WaitForSeconds(1.5f);
        this.transform.parent.gameObject.SetActive(false);
    }
    protected override void  ShowCoinText()
    {
        this.CoinText.text  = "+" + CoinUISpawner.Instance.CurrentNumberofCoins.ToString();
    } 
}
