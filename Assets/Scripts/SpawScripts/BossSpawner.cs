using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossSpawner : Spawner
{
    protected static BossSpawner instance;
    public static BossSpawner Instance { get => instance ;}
    public List<string> ListBossesname;
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
            if(ListBossesname.Count < prefabs.Count) ListBossesname.Add("");
            ListBossesname[i] = prefabs[i].name;
        }
    }
    public override Transform Spawn(string PrefabName, Vector3 position, Quaternion rotation)
    {
        Transform newPre = base.Spawn(PrefabName, position,rotation);
        //HPbar
        Transform HpbarTrans =  SpawnEnemieHpbar(newPre);
        EnemieHpbar Hpbar = HpbarTrans.GetComponentInChildren<EnemieHpbar>();
        HpbarTrans.GetComponent<RectTransform>().localPosition = new Vector3(0,-400,0);
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
        Transform Hpbarpre = BossHpbarUISpawner.Instance.Spawn(BossHpbarUISpawner.HpbarEnum.BossHpbar.ToString(),Vector3.zero,Quaternion.identity);
        return Hpbarpre;
    }
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
}
