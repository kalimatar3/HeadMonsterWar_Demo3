using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MyBehaviour
{
    protected static MapManager instance;
    public static MapManager Instance { get => instance ;}
    public List<Vector3> ListBossSapwnPos;
    public List<Vector3> ListEnemySpawnPos;
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
    protected override void LoadComponents()
    {
       base.LoadComponents();
    }
    public void LoadMap(string Mapname)
    {
        Transform thisMap = null;
        string path = "Maps/" + Mapname;
        if(Resources.Load<Transform>(path) == null) Debug.LogWarning(this.transform + "Can load Resources " + Mapname);
        foreach(Transform element in this.transform)
        {
            if(Mapname == element.name)
            {
                thisMap = element;
                return;
            }
        }
        thisMap = Instantiate(Resources.Load<Transform>(path));
        thisMap.name = Mapname;
        thisMap.transform.SetParent(this.transform);
        Transform bossspawnPos = thisMap.transform.Find("BossSpawnPos");
        foreach(Transform element in bossspawnPos)
        {
            ListBossSapwnPos.Add(element.position);
        }
        Transform EnemySpawnTrans = thisMap.transform.Find("EnemySpawnPos");
        foreach(Transform element in EnemySpawnTrans)
        {
            ListEnemySpawnPos.Add(element.position);
        }
    }
    protected void SpawnMap(string Mapname)
    {
    }
    public IEnumerator DelayLoadMap()
    {
        yield return new WaitUntil( predicate: ()=>
        {
            if(DataManager.Instance.CurrentMap == null) return false;
            else return true;
        });
        this.LoadMap(DataManager.Instance.CurrentMap);
    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(this.DelayLoadMap());
    }
}
