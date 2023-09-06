using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SpawnEnemies : MyBehaviour
{
    [SerializeField] protected List<int> NumberOfEachEnemy;
    [SerializeField] protected List<int> NUmberOfEachBoss;
    [SerializeField] protected List<Vector2> TimeToSpawn;
    public List<Transform> ListEnemies,ListBosses;
    [SerializeField] protected List<Vector3> ListEnemyCanSpawnPos;
    [SerializeField] protected Transform CurrentBoss;
    [SerializeField] int BossinWave;
    protected float timer;
    protected int thisEnemie,thistime;
    public float NumberofPreEnemies,NumberofAliveEnemies;
    public int MaxNumberofEnemies;
    [SerializeField] protected Vector3 ThisPos;
    [SerializeField] protected bool tagGate,pointgate;
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
            BossinWave += NUmberOfEachBoss[i];
        }
    }
    protected void OnEnable()
    {
        tagGate = false;
        pointgate = false;
        LevelManager.Instance.WaveTag.gameObject.SetActive(true);
        LevelManager.Instance.Leveltag.gameObject.SetActive(true);
        StartCoroutine( LevelManager.Instance.WaveTag.GetComponent<waveTagPerform>().StartWavePerform(this.transform.name));
        StartCoroutine(SpawnNorPointDelay());
    }
    protected IEnumerator SpawnBossPointDelay()
    {
        yield return new WaitUntil(predicate:()=>
        {
            return CurrentBoss.gameObject.activeInHierarchy;
        });
        Transform Pointer = BossPointSpawner.Instance.Spawn(BossPointSpawner.BossPointEnum.BossPoint.ToString(),CurrentBoss.position,Quaternion.identity);
        Pointer.GetComponentInChildren<BossPoint>().Obj = CurrentBoss;
        Pointer.GetComponentInChildren<EnePointDEspawn>().Obj = CurrentBoss;
    }
    protected IEnumerator SpawnNorPointDelay()
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(predicate:()=>
        {
            if(NumberofPreEnemies > 0) return false;
            return true;
        });
        yield return new WaitUntil(predicate:()=>
        {
            if(NumberofAliveEnemies  > 3 ) return false;
            return true;
        });
        foreach(Transform element in ListEnemies)
        {
            if(element.gameObject.activeInHierarchy) 
            {
                Transform Pointer = BossPointSpawner.Instance.Spawn(BossPointSpawner.BossPointEnum.EnemyPoint.ToString(),element.transform.position,Quaternion.identity);
                Pointer.GetComponentInChildren<EnePointDEspawn>().Obj = element;
                Pointer.GetComponentInChildren<BossPoint>().Obj = element;
            }
        }
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
    protected void AddToLIstBoss(Transform Boss)
    {
        for(int i = 0 ; i < ListBosses.Count ; i ++)
        {
            if(ListBosses[i] == Boss) return;
        }
        ListBosses.Add(Boss);
    }
    protected void spawnenemie()
    {
        thistime  = Random.Range((int)TimeToSpawn[thisEnemie].x,(int)TimeToSpawn[thisEnemie].y +1); 
        thisEnemie = Random.Range(0,NumberOfEachEnemy.Count);
        timer += Time.deltaTime *1f;
        if(timer >= thistime && NumberOfEachEnemy[thisEnemie] != 0)
        {
            this.EnemySpawnPos();
            timer = 0 ;
            Transform NewEne = EnemiesSpawner.Instance.Spawn(EnemiesSpawner.Instance.EnemiesName[thisEnemie],ThisPos,Quaternion.identity);
            AddtoListEnemies(NewEne);
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
    protected void EnemySpawnPos()
    {
        foreach(Vector3 elment in MapManager.Instance.ListEnemySpawnPos)
        {
            Vector3 Dir = elment - PlayerController.Instance.transform.position;
            if(Dir.magnitude >= 35f)    ListEnemyCanSpawnPos.Add(elment);        
        }
        float Min = 10000f;
        foreach(Vector3 ele in ListEnemyCanSpawnPos)
        {
            Vector3 Dir = ele - PlayerController.Instance.transform.position;
            if(Dir.magnitude <= Min) 
            {
                ThisPos = ele;
                Min = Dir.magnitude;
            }
        }
    }
    protected IEnumerator FreezeEnemies()
    {
        foreach(Transform ele in ListEnemies)
        {
            ele.GetComponentInChildren<TrackPlayer>().gameObject.SetActive(false);
            ele.GetComponentInChildren<EnemieAct>().gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1f);
        foreach(Transform ele in ListEnemies)
        {
            ele.GetComponentInChildren<TrackPlayer>().gameObject.SetActive(true);
            ele.GetComponentInChildren<EnemieAct>().gameObject.SetActive(true);
        }

    }
    protected void spawnboss()
    {
        thistime  = 3; 
        if(CurrentBoss == null || !CurrentBoss.gameObject.activeInHierarchy)
        {
            thisEnemie = Random.Range(0,NUmberOfEachBoss.Count);
            timer += Time.deltaTime *1f;
            if(timer >= thistime && NUmberOfEachBoss[thisEnemie] != 0)
            {
                timer = 0 ;
                int rdPos = Random.Range(0,MapManager.Instance.ListBossSapwnPos.Count);
                ThisPos = MapManager.Instance.ListBossSapwnPos[rdPos]; 
                CurrentBoss = BossSpawner.Instance.Spawn(BossSpawner.Instance.ListBossesname[thisEnemie],ThisPos,Quaternion.identity);
                this.StartCoroutine(SpawnBossPointDelay());
                this.StartCoroutine(FreezeEnemies());
                AddToLIstBoss(CurrentBoss);
                NUmberOfEachBoss[thisEnemie]--;
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
        if(CurrentBoss == null && BossinWave > 0) return;
        spawnenemie();
    }
    protected void CountEnemy()
    {
        float cache1 = 0,cache2 = 0;
        for(int i = 0 ; i < NUmberOfEachBoss.Count;i++) cache1 += NUmberOfEachBoss[i]; 
        for(int i = 0 ; i < NumberOfEachEnemy.Count;i++) cache1 += NumberOfEachEnemy[i]; 
        this.NumberofPreEnemies = cache1;
        for(int i = 0 ; i < ListBosses.Count ; i++)
        {
            if(ListBosses[i].gameObject.activeInHierarchy) cache2 ++;
        }
        for(int i = 0 ; i < ListEnemies.Count ; i++)
        {
            if(ListEnemies[i].gameObject.activeInHierarchy) cache2 ++;
        }
        this.NumberofAliveEnemies = cache2;
        if(NumberofAliveEnemies + NumberofPreEnemies <= 0 && tagGate == false)  
        {
            tagGate = true;
            LevelManager.Instance.WaveTag.gameObject.SetActive(true);
            StartCoroutine( LevelManager.Instance.WaveTag.GetComponent<waveTagPerform>().ENdWavePerform(this.transform.gameObject.name));
        }
    }
}
