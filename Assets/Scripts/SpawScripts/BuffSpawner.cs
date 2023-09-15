using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpawner : Spawner
{
    protected static BuffSpawner instance;
    public static BuffSpawner Instance { get => instance ;}
    public List<string> Buffname;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuffName();
    }
    protected void LoadBuffName()
    {
        for(int  i = 0 ; i < prefabs.Count; i++)
        {
            if(Buffname.Count < prefabs.Count) Buffname.Add("");
            Buffname[i] = prefabs[i].name;
        }
    }
    public override Transform Spawn(string PrefabName, Vector3 position, Quaternion rotation)
    {
        Transform Newpre = base.Spawn(PrefabName,position,rotation);
        BufftoPlayer bufftoPlayer   = Newpre.GetComponentInChildren<BufftoPlayer>();
        foreach(DropItemSO element in DataManager.Instance.ListDropItemSO)
        {
            if(PrefabName == element.Name)
            {
                bufftoPlayer.dealnumber = element.Quality[DataManager.Instance.GetUpgradenumberfromDID(element.Name)];
            }
        }
        if(bufftoPlayer == null) Debug.LogWarning(Newpre + "can found buff");
        return Newpre;
    }
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }

}
