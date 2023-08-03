using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDealer : DealToEnemies
{
    protected override void SendDametoObj(Transform obj)
    {
        EnemiesReciver enemiesReciver =  obj.transform.GetComponent<EnemiesReciver>();
        if(enemiesReciver == null) return;
        foreach(string element in EnemiesSpawner.Instance.EnemiesName)
        {
            if(element == obj.transform.parent.name)  
            {
            EnemiesSpawner.Instance.ListEnemiesDefectSpawn.Add(obj.transform.parent);
            EnemiesSpawner.Instance.DeSpawnToPool(obj.transform.parent);
            }

        }
        foreach(string element in BossSpawner.Instance.ListBossesname)
        {
            if(element == obj.transform.parent.name)   
            { 
                BossSpawner.Instance.ListEnemiesDefectSpawn.Add(obj.transform.parent);
                BossSpawner.Instance.DeSpawnToPool(obj.transform.parent);
            }
        }
    }
}
