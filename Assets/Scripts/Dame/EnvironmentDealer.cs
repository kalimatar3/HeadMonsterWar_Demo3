using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDealer : DealToEnemies
{
    protected override void SendDametoObj(Transform obj)
    {
        NorEnemyReciver enemiesReciver =  obj.transform.GetComponent<NorEnemyReciver>();
        if(enemiesReciver == null) return;
        foreach(string element in EnemiesSpawner.Instance.EnemiesName)
        {
            if(element == obj.transform.parent.name)  
            {
                EnemiesSpawner.Instance.ListEnemiesDefectSpawn.Add(obj.transform.parent);
                EnemiesSpawner.Instance.DeSpawnToPool(obj.transform.parent);
            }

        }
    }
}
