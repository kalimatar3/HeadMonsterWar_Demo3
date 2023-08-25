using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MyBehaviour
{
    [SerializeField] protected List<int> NumberOfEachEnemy;
    [SerializeField] protected List<int> NUmberOfEachBoss;
    [SerializeField] protected List<Vector2> DisAroundPlayer;
    [SerializeField] protected List<Vector2> TimeToSpawn;
    public List<Transform> ListEnemies;
    protected float timer;
    protected int thisEnemie,thistime,EnemyInWave,BossinWave;
    public float NumberofPreEnemies,NumberofAliveEnemies;
    public int MaxNumberofEnemies;
    protected Vector3 ThisPos;
    protected bool Gate;
    protected override void Start()
    {
        base.Start();
        for(int i = 0 ; i <NumberOfEachEnemy.Count ; i++ )
        {
            MaxNumberofEnemies += NumberOfEachEnemy[i];
        }
        for(int i = 0 ; i < NUmberOfEachBoss.Count ;i++)
        {
            MaxNumberofEnemies += NUmberOfEachBoss[i];
        }
    }
    protected void OnEnable()
    {
        Gate = false;
        LevelManager.Instance.WaveTag.gameObject.SetActive(true);
        LevelManager.Instance.Leveltag.gameObject.SetActive(true);
        StartCoroutine( LevelManager.Instance.WaveTag.GetComponent<waveTagPerform>().StartWavePerform(this.transform.name));
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
    protected void AddtoListEnemies(Transform Enemy)
    {
        for(int i = 0 ; i < ListEnemies.Count ; i ++)
        {
            if(ListEnemies[i] == Enemy) return;
        }
        ListEnemies.Add(Enemy);
    }
    protected void spawnenemie()
    {
        thistime  = Random.Range((int)TimeToSpawn[thisEnemie].x,(int)TimeToSpawn[thisEnemie].y +1); 
        thisEnemie = Random.Range(0,NumberOfEachEnemy.Count);
        timer += Time.deltaTime *1f;
        if(timer >= thistime && NumberOfEachEnemy[thisEnemie] != 0)
        {
            timer = 0 ;
            if(thisEnemie > 1) 
            {
                int rdPos = Random.Range(0,MapManager.Instance.ListBossSapwnPos.Count);
                ThisPos = MapManager.Instance.ListBossSapwnPos[rdPos]; 
            }
            else ThisPos = RandomPosAroundPLayer(DisAroundPlayer[thisEnemie]);
            AddtoListEnemies(EnemiesSpawner.Instance.Spawn(EnemiesSpawner.Instance.EnemiesName[thisEnemie],ThisPos,Quaternion.identity));
            NumberOfEachEnemy[thisEnemie]--;
        }
        if(EnemiesSpawner.Instance.ListEnemiesDefectSpawn.Count > 0 )
        {
            for(int i = 0 ; i < EnemiesSpawner.Instance.ListEnemiesDefectSpawn.Count ;i ++)
            {
                if(EnemiesSpawner.Instance.ListEnemiesDefectSpawn[i].name == EnemiesSpawner.Instance.EnemiesName[thisEnemie])
                {
                    NumberOfEachEnemy[thisEnemie]++;
                    EnemiesSpawner.Instance.ListEnemiesDefectSpawn.Remove(EnemiesSpawner.Instance.ListEnemiesDefectSpawn[i]);
                }
            }
        }
    }
    protected void spawnboss()
    {
        thistime  = 3; 
        thisEnemie = Random.Range(0,NUmberOfEachBoss.Count);
        timer += Time.deltaTime *1f;
        if(timer >= thistime && NUmberOfEachBoss[thisEnemie] != 0)
        {
            timer = 0 ;
            int rdPos = Random.Range(0,MapManager.Instance.ListBossSapwnPos.Count);
            ThisPos = MapManager.Instance.ListBossSapwnPos[rdPos]; 
            Transform NewBoss  = BossSpawner.Instance.Spawn(BossSpawner.Instance.ListBossesname[thisEnemie],ThisPos,Quaternion.identity);
            Transform Pointer = BossPointSpawner.Instance.Spawn(BossPointSpawner.BossPointEnum.BossPoint.ToString(),PlayerController.Instance.transform.position,Quaternion.identity);
            Pointer.GetComponentInChildren<DespawnbyObj>().Obj = NewBoss;
            Pointer.GetComponentInChildren<BossPoint>().Obj = NewBoss;
            ListEnemies.Add(NewBoss);
            NUmberOfEachBoss[thisEnemie]--;
        }
        if(BossSpawner.Instance.ListEnemiesDefectSpawn.Count > 0 )
        {
            for(int i = 0 ; i < BossSpawner.Instance.ListEnemiesDefectSpawn.Count ;i ++)
            {
                if(BossSpawner.Instance.ListEnemiesDefectSpawn[i].name == BossSpawner.Instance.ListBossesname[thisEnemie])
                {
                    NUmberOfEachBoss[thisEnemie]++;
                    BossSpawner.Instance.ListEnemiesDefectSpawn.Remove(BossSpawner.Instance.ListEnemiesDefectSpawn[i]);
                }
            }
        }
    }
    protected void FixedUpdate()
    {
        this.WaveSpawn();
        this.CountEnemy();
    }
    protected void WaveSpawn()
    {
        this.spawnboss();
        if(BossinWave > 0) return;
        spawnenemie();
    }
    protected void CountEnemy()
    {
        float cache1 = 0,cache2 = 0;
        for(int i = 0 ; i < NUmberOfEachBoss.Count;i++) cache1 += NUmberOfEachBoss[i]; 
        for(int i = 0 ; i < NumberOfEachEnemy.Count;i++) cache1 += NumberOfEachEnemy[i]; 
        this.NumberofPreEnemies = cache1;
        for(int i = 0 ; i < ListEnemies.Count ; i++)
        {
            if(ListEnemies[i].gameObject.activeInHierarchy) cache2 ++;
        }
        this.NumberofAliveEnemies = cache2;
        if(NumberofAliveEnemies + NumberofPreEnemies <= 0 && Gate == false)  
        {
            Gate = true;
            LevelManager.Instance.WaveTag.gameObject.SetActive(true);
            StartCoroutine( LevelManager.Instance.WaveTag.GetComponent<waveTagPerform>().ENdWavePerform(this.transform.gameObject.name));
        }
    }
}
