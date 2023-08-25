using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPointSpawner : Spawner
{
    protected static BossPointSpawner instance;
    public static BossPointSpawner Instance { get => instance ;}
    [SerializeField] protected List<string> BossPointName;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossPoint();
    }
    public enum BossPointEnum
    {
        BossPoint
    }
    protected void LoadBossPoint()
    {
        for(int  i = 0 ; i < prefabs.Count; i++)
        {
            if(BossPointName.Count < prefabs.Count) BossPointName.Add("");
            BossPointName[i] = prefabs[i].name;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
}
