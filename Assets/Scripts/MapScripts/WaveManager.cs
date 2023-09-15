using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveManager : MyBehaviour
{    
    [SerializeField] protected List<Transform> ListWave;
    [SerializeField] protected int CurrentWave;
    [SerializeField] protected float preparetime;
    protected string cache;
    public float CoinLevelReward;
    protected float timer;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWaves();
    }
    protected void LoadWaves()
    {
        if(ListWave.Count != 0 ) return;
        foreach(Transform wave in this.transform)
        {
            ListWave.Add(wave);
        }
    }
    protected void OnEnable()
    {
        this.StartCoroutine(this.DelayLoadRewardCoin());        
    }
    protected IEnumerator DelayLoadRewardCoin()
    {
        yield return new WaitUntil(predicate :()=>
        {
            if(CoinUISpawner.Instance == null) return false;
            return true;
        });
        CoinUISpawner.Instance.CurrentNumberofCoins = (int)this.CoinLevelReward;
    }
    protected virtual void ChangeLvInEmty()
    {
        foreach(Transform Wave in ListWave)
        {
            if(Wave.gameObject.activeInHierarchy)
            {
                LevelManager.Instance.CrLevelname = Wave.name;
                SpawnEnemies spawnEnemies = Wave.GetComponent<SpawnEnemies>();
                if(spawnEnemies == null) return;
                LevelManager.Instance.NumberofPreCE = spawnEnemies.NumberofPreEnemies;
                LevelManager.Instance.NumberofAllCE = spawnEnemies.MaxNumberofEnemies;
                LevelManager.Instance.NumberofAliveCE = spawnEnemies.NumberofAliveEnemies;
                if(LevelManager.Instance.NumberofPreCE > 0) return;
                if(Wave.GetComponent<SpawnEnemies>() == null) return;
               foreach(Transform enemie in Wave.GetComponent<SpawnEnemies>().ListEnemies)
                if(enemie.gameObject.activeInHierarchy == true)  return;
                foreach(Transform enemie in Wave.GetComponent<SpawnEnemies>().ListBosses)
                if(enemie.gameObject.activeInHierarchy == true)  return;
            }
        }
        timer += Time.deltaTime * 1f;
        if(timer > preparetime)
        {
            timer = 0;
            foreach(Transform element in ListWave)  element.gameObject.SetActive(false);
            ListWave[CurrentWave].gameObject.SetActive(true);
            CurrentWave ++;
            if(CurrentWave >= ListWave.Count)
            {
                ManagerAds.ins.ShowInterstitial();
                PanelCtrl.Instance.ShowPanel("Winpannel");
                LevelManager.Instance.NextLevel();
                this.transform.parent.gameObject.SetActive(false);
            }
        }
    }
    protected void FixedUpdate()
    {
        this.ChangeLvInEmty();
    }
}
