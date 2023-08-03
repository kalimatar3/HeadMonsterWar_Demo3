using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieActSpawner : Spawner
{
    protected static EnemieActSpawner instance;
    public static EnemieActSpawner Instance { get => instance ;}
    [SerializeField] protected List<string> Bulletname;
    public enum ActEnum
    {
        Punch,
        Explode,
        LazerHit,
        BossPunch,

    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletName();
    }
    protected void LoadBulletName()
    {
        for(int  i = 0 ; i < prefabs.Count; i++)
        {
            if(Bulletname.Count < prefabs.Count) Bulletname.Add("");
            Bulletname[i] = prefabs[i].name;
        }
    }
    public override Transform Spawn(string PrefabName, Vector3 position, Quaternion rotation)
    {
        Transform newPre = base.Spawn(PrefabName,position,rotation);
        EnemyDealToPlayer dealToPlayer = newPre.GetComponentInChildren<EnemyDealToPlayer>();
        foreach(EnemiesSO ThisSO in DataManager.Instance.ListEnemieSO)
        {
            if(ThisSO.SkillName == PrefabName)
            {
                dealToPlayer.dealnumber = ThisSO.Dame;
            }
        }
        return newPre;
    }
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }

}
