using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpbarSpawner : Spawner
{
    protected static HpbarSpawner instance;
    public static HpbarSpawner Instance { get => instance ;}
    [SerializeField] protected List<string> HPbarName;
    public enum HpbarEnum
    {
        EnemieHpbar,
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
