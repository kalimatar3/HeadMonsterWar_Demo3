using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUIManager : MyBehaviour
{
    protected static CoinUIManager instance;
    public static CoinUIManager Instance { get => instance ;}
    [SerializeField] Transform CoinStart,CoinEnd;
    [SerializeField] Text CoinText;
    public float CurrentCoinTextNumber;
    protected float timer;
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
    protected override void LoadComponents()
    {   
        base.LoadComponents();
        this.LoadCoinNmber();
    }
    protected void LoadCoinNmber()
    {
        CoinText = GetComponentInChildren<Text>();
    }
    protected void OnEnable()
    {
        this.CurrentCoinTextNumber = 0;
        this.StartCoroutine(Delayspawncoins());
    }
    protected IEnumerator Delayspawncoins()
    {
        yield return new WaitUntil(predicate : ()=>
        {
            if(CoinUISpawner.Instance == null) return false;
            return true;
        }); 
        int sum = 0;
        timer = 0; 
        while( sum < CoinUISpawner.Instance.CurrentNumberofCoins)
        {
            Transform Coin =  CoinUISpawner.Instance.Spawn("Coin",this.CoinStart.position,Quaternion.identity);
            CoinUIManager.instance.CurrentCoinTextNumber += 5;
            CoinFly thisCoinfly = Coin.GetComponentInChildren<CoinFly>();
            thisCoinfly.SpawnPos = this.CoinStart.position;
            thisCoinfly.StartPos = RandomPosAroundObj(CoinStart,new Vector2(1,2)); 
            thisCoinfly.EndPos = CoinEnd.position;
            sum ++;
            yield return new WaitForSeconds(0.2f);
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
    protected void FixedUpdate()
    {
        this.CoinText.text  = CurrentCoinTextNumber.ToString();
    }
}

