using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MyBehaviour
{    
    [SerializeField] protected List<Transform> ListWave;
    [SerializeField] protected int CurrentWave;
    [SerializeField] protected float preparetime;
    public float CoinLevelReward;
    protected float timer;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadlevels();
    }
    protected void Loadlevels()
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
        CoinUISpawner.Instance.CurrentNumberofCoins = (int)this.CoinLevelReward/(int)5;
    }
    protected virtual void ChangeLvInEmty()
    {
        foreach(Transform level in ListWave)
        {
            if(level.gameObject.activeInHierarchy)
            {
                LevelManager.Instance.CrLevelname = level.name;
                SpawnEnemies spawnEnemies = level.GetComponent<SpawnEnemies>();
                if(spawnEnemies == null) return;
                LevelManager.Instance.NumberofPreCE = spawnEnemies.NumberofPreEnemies;
                LevelManager.Instance.NumberofAllCE = spawnEnemies.MaxNumberofEnemies;
                LevelManager.Instance.NumberofAliveCE = spawnEnemies.NumberofAliveEnemies;
                if(LevelManager.Instance.NumberofPreCE > 0) return;
                if(level.GetComponent<SpawnEnemies>() == null) return;
               foreach(Transform enemie in level.GetComponent<SpawnEnemies>().ListEnemies)
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
                PanelCtrl.Instance.ShowPanel("Winpannel");
                LevelManager.Instance.NextLevel();
            }
        }
    }
    protected void FixedUpdate()
    {
        this.ChangeLvInEmty();
    }

}
