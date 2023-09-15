using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUISpawner : Spawner
{
    protected static CoinUISpawner instance;
    public static CoinUISpawner Instance { get => instance ;}
    [SerializeField] public float CurrentNumberofCoins;    
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
    public override Transform Spawn(string PrefabName, Vector3 position, Quaternion rotation)
    {
        Transform newPre = base.Spawn("Coin",position,rotation);
        return newPre;
    }
}
