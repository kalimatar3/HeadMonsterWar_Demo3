using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EffectSpawner : Spawner
{
    public List<Transform> Prefabs => base.prefabs;
    protected static EffectSpawner instance;
    public static EffectSpawner Instance { get => instance ;}
    [SerializeField] protected List<string> EffectName;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEffectName();
    }
    protected void LoadEffectName()
    {
        for(int  i = 0 ; i < prefabs.Count; i++)
        {
            if(EffectName.Count < prefabs.Count) EffectName.Add("");
            EffectName[i] = prefabs[i].name;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
}
