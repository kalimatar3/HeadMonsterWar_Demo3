using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MyBehaviour
{
    [SerializeField] protected List<int> NumberOfEachEnemie;
    [SerializeField] protected List<Vector2> DisAroundPlayer;
    [SerializeField] protected List<Vector2> TimeToSpawn;
    public List<Transform> ListEnemies;
    protected float timer;
    protected int thisEnemie,thistime;
    public int AllEnemieinlevel;
    public int MaxNumberofEnemies;
    protected override void Start()
    {
        base.Start();
        for(int i = 0 ; i <NumberOfEachEnemie.Count ; i++ )
        {
            MaxNumberofEnemies += NumberOfEachEnemie[i];
        }
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    protected Vector3 RandomPosAroundPLayer(Vector2 Radius)
    {
        int Randradius = Random.Range((int)Radius.x,(int)Radius.y +1);
        Vector3 ranPos = Vector3.zero;
        float direc = Random.Range(0,2);
        float thisradius = Random.Range(-Randradius,Randradius + 1);
        if(direc == 0)  ranPos = new Vector3 (thisradius,0,Mathf.Sqrt(Randradius * Randradius - thisradius* thisradius));
        else  ranPos = new Vector3 (thisradius,0,- Mathf.Sqrt(Randradius * Randradius - thisradius* thisradius));
        return PlayerController.Instance.transform.position + ranPos;
    }
    protected void spawnenemie()
    {
        int cache = 0;
        thistime  = Random.Range((int)TimeToSpawn[thisEnemie].x,(int)TimeToSpawn[thisEnemie].y);
        thisEnemie = Random.Range(0,NumberOfEachEnemie.Count);
        this.StartCoroutine(DelaySpawnEnemy());
        if(timer >= thistime && NumberOfEachEnemie[thisEnemie] != 0)
        {
            timer = 0 ;
            Vector3 thisPos = RandomPosAroundPLayer(DisAroundPlayer[thisEnemie]);
            ListEnemies.Add(EnemiesSpawner.Instance.Spawn(EnemiesSpawner.Instance.EnemiesName[thisEnemie],thisPos,Quaternion.identity));
            NumberOfEachEnemie[thisEnemie]--;
        }
        if(EnemiesSpawner.Instance.ListEnemiesDefectSpawn.Count > 0 )
        {
            for(int i = 0 ; i < EnemiesSpawner.Instance.ListEnemiesDefectSpawn.Count ;i ++)
            {
                if(EnemiesSpawner.Instance.ListEnemiesDefectSpawn[i].name == EnemiesSpawner.Instance.EnemiesName[thisEnemie])
                {
                    NumberOfEachEnemie[thisEnemie]++;
                    MaxNumberofEnemies ++;
                    EnemiesSpawner.Instance.ListEnemiesDefectSpawn.Remove(EnemiesSpawner.Instance.ListEnemiesDefectSpawn[i]);
                }
            }
        }
        for(int i = 0 ; i < NumberOfEachEnemie.Count;i++) cache += NumberOfEachEnemie[i]; 
         AllEnemieinlevel = cache;
    }
    protected IEnumerator DelaySpawnEnemy()
    {
        yield return new WaitUntil(predicate: () =>
        {
            if(CameraCtrl.Instance.CameraFollow.Obj != PlayerController.Instance.transform) return false;
            else return true;
        });
        timer += Time.deltaTime *1f;
    }
    protected void FixedUpdate()
    {
        this.spawnenemie();
    }
}
