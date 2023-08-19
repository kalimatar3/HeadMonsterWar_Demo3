using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemiesSpawner : Spawner
{
    protected static EnemiesSpawner instance;
    public static EnemiesSpawner Instance { get => instance ;}
    public List<string> EnemiesName;
    public List<Transform> ListEnemiesDefectSpawn;
    public enum EnemiesEnum
    {
        Enemie_01,
        Enemie_02,
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemieName();
    }
    protected void LoadEnemieName()
    {
        for(int  i = 0 ; i < prefabs.Count; i++)
        {
            if(EnemiesName.Count < prefabs.Count) EnemiesName.Add("");
            EnemiesName[i] = prefabs[i].name;
        }
    }
    public override Transform Spawn(string PrefabName, Vector3 position, Quaternion rotation)
    {
        Transform newPre = base.Spawn(PrefabName, position,rotation);
        //HPbar
        Transform HpbarTrans =  SpawnEnemieHpbar(newPre);
        HpbarTrans.transform.localScale = newPre.localScale;
        HpbarTrans.GetComponentInChildren<followObj>().Obj = newPre;
        HpbarTrans.gameObject.SetActive(true);
        HpbarTrans.GetComponentInChildren<DeSpawnHpbar>().Obj = newPre;
        EnemieHpbar Hpbar = HpbarTrans.GetComponentInChildren<EnemieHpbar>();
        // SO
        EnemiesReciver enemiesReciver = newPre.GetComponentInChildren<EnemiesReciver>();
        foreach(EnemiesSO thisSO in DataManager.Instance.ListEnemieSO )
        {
            if(thisSO.name == PrefabName) 
            {
                enemiesReciver.MaxHp = thisSO.maxhp;
                enemiesReciver.CurrentHp = thisSO.maxhp;
                newPre.GetComponent<EnemieCtrl>().SpawnDropItem.BaseListDropRate = thisSO.ListDroprate;
            }
        }
        Hpbar.DameReciver = enemiesReciver.transform;
        return newPre;
    }
    protected virtual Transform SpawnEnemieHpbar(Transform Obj)
    {
        Transform Hpbarpre = HpbarSpawner.Instance.Spawn(HpbarSpawner.HpbarEnum.EnemieHpbar.ToString(),Obj.transform.parent.position ,Quaternion.identity);
        return Hpbarpre;
    }
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
}
