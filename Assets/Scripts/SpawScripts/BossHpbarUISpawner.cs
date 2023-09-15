using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpbarUISpawner : Spawner
{
    protected static BossHpbarUISpawner instance;
    public static BossHpbarUISpawner Instance { get => instance ;}
    [SerializeField] protected List<string> HPbarName;
    public enum HpbarEnum
    {
        BossHpbar,
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossHPbarName();
    }
    protected void LoadBossHPbarName()
    {
        for(int  i = 0 ; i < prefabs.Count; i++)
        {
            if(HPbarName.Count < prefabs.Count) HPbarName.Add("");
            HPbarName[i] = prefabs[i].name;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }


}
