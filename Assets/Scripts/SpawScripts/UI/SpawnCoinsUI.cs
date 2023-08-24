using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCoinsUI : MyBehaviour
{
    [SerializeField] protected Transform CoinStart,CoinEnd;
    [SerializeField] protected Text CoinText;
    public float CurrentCoinTextNumber;
    protected override void LoadComponents()
    {   
        base.LoadComponents();
        this.LoadCoinNmber();
    }
    protected void LoadCoinNmber()
    {
        CoinText = GetComponentInChildren<Text>();
    }
    protected virtual void OnEnable()
    {
        this.StartCoroutine(spawncoinsDelay());
    }
    public virtual IEnumerator spawncoinsDelay()
    {
        this.CurrentCoinTextNumber = 0;
        yield return new WaitUntil(predicate : ()=>
        {
            if(CoinUISpawner.Instance == null) return false;
            return true;
        }); 
        int sum = 0;
        while( sum < CoinUISpawner.Instance.CurrentNumberofCoins)
        {
            CurrentCoinTextNumber ++;
            sum ++;
            yield return new WaitForSeconds(1f/CoinUISpawner.Instance.CurrentNumberofCoins);
        }
        sum = 0;
        while( sum < 10)
        {
            SoundSpawner.Instance.Spawn(CONSTSoundsName.PickCoin,this.transform.position,Quaternion.identity);
            Transform Coin =  CoinUISpawner.Instance.Spawn("Coin",this.CoinStart.position,Quaternion.identity);
            CoinFly thisCoinfly = Coin.GetComponentInChildren<CoinFly>();
            thisCoinfly.SpawnPos = this.CoinStart.position;
            thisCoinfly.StartPos = RandomPosAroundObj(CoinStart,new Vector2(1,2)); 
            thisCoinfly.EndPos = CoinEnd.position;
            sum ++;
            yield return new WaitForSeconds(0.1f);
        }
    }
     protected Vector3 RandomPosAroundObj(Transform Obj,Vector2 Radius)
    {
        int Randradius = Random.Range((int)Radius.x,(int)Radius.y +1);
        Vector3 ranPos = Vector3.zero;
        float direc = Random.Range(0,2);
        float thisradius = Random.Range(-Randradius,Randradius + 1);
        if(direc == 0)  ranPos = new Vector3 (thisradius,Mathf.Sqrt(Randradius * Randradius - thisradius* thisradius),0);
        else  ranPos = new Vector3 (thisradius,- Mathf.Sqrt(Randradius * Randradius - thisradius* thisradius),0);
        return Obj.transform.position + ranPos;
    }
    protected virtual void ShowCoinText()
    {
        this.CoinText.text  = "+" + CurrentCoinTextNumber.ToString();
    } 
    protected void FixedUpdate()
    {
        this.ShowCoinText();
    }
}

