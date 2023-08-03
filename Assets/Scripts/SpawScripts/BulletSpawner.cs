using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Spawner
{
    protected static BulletSpawner instance;
    public static BulletSpawner Instance { get => instance ;}
    [SerializeField] protected List<string> Bulletname;
    public float ExtraDame;
    public enum BulletEnum
    {
        Shotgunbullet,
        Pistolbullet,
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
        DealToEnemies dealToEnemies = newPre.GetComponentInChildren<DealToEnemies>();
        foreach(BulletsSO ThisSO in DataManager.Instance.ListBulletsSO)
        {
            if(ThisSO.name == PrefabName)
            {
                dealToEnemies.CanThroughObj = ThisSO.CanThroughObj;
                dealToEnemies.dealnumber = ThisSO.Dame ;
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
