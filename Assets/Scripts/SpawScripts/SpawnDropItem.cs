using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDropItem : MyBehaviour
{
    public List<float> CheckList;
    public List<float> BaseListDropRate;
    [SerializeField] protected List<float> CurrentListDropRate;
    protected bool Gate;
    [SerializeField] protected  List<float> Element;
    protected override void Start()
    {
        base.Start();
        StartCoroutine(this.DropRateChanceDelay());
    }
    protected void OnEnable()
    {
        Element.Clear();
    }
    protected void LoadCurrentListDropRate()
    {
        for(int i = 0 ; i < BaseListDropRate.Count ; i ++)
        {
            CurrentListDropRate.Add(BaseListDropRate[i]);
        }
    }
    public void spawnBuff()
    {
        Element = Rand.Main(CurrentListDropRate);
        if(Element[0] <= 0) return;
        BuffSpawner.Instance.Spawn(BuffSpawner.Instance.Buffname[(int)Element[0]],this.transform.parent.position,Quaternion.identity);
        BuffManager.Instance.IncreaseCurrentNumberofBuff(BuffSpawner.Instance.Buffname[(int)Element[0]]);
    }
    protected void DropRateChance()
    {
        for(int i = 0 ; i< BuffManager.Instance.ListCurrentNumberofBuff.Count; i++)
        {
            if(BuffManager.Instance.ListCurrentNumberofBuff[i] >= BuffManager.Instance.ListLimitNumberofBuff[i])
            this.CurrentListDropRate[i] = 0;
            else
            { 
                if(i == 2 )  CurrentListDropRate[i] = (BaseListDropRate[i] + 100 - ((float)PlayerController.Instance.PlayerReciver.CurrentHp/(float)PlayerController.Instance.PlayerReciver.MaxHp)*100)/2;
                if(i > 2)
                {
                    this.CurrentListDropRate[i] = this.BaseListDropRate[i];
                }
            }
        }
        float HPHealDropRate = 100;
        for(int i = 0 ; i  < BaseListDropRate.Count ; i++)
        {
           if(i != 1)    HPHealDropRate = HPHealDropRate - CurrentListDropRate[i];
        }
        CurrentListDropRate[1] = HPHealDropRate;
        for(int i = 0 ; i < CurrentListDropRate.Count ; i++)
        {
            if(CurrentListDropRate[i] >= 100) CurrentListDropRate[i] = 100;
            if(CurrentListDropRate[i] < 0) CurrentListDropRate[i] = 0;
        }
    }
    protected IEnumerator DropRateChanceDelay()
    {
        yield return new WaitUntil(predicate : ()=>
        {
            if(BaseListDropRate.Count <= 0) return false;
            else return true;
        });
        this.LoadCurrentListDropRate();
        Gate = true;
    }
    protected void FixedUpdate()
    {
       if(Gate)
       {
        this.DropRateChance();
       } 
    }
}
