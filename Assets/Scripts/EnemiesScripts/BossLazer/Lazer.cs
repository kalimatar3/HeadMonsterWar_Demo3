using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : EnemieAct
{
    [SerializeField] protected float PreShootTime,ShootingTime;
    [SerializeField] protected LayerMask LazerCanHit;
    [SerializeField] protected Transform TarGet;
    protected Transform DamePoint;
    protected LineRenderer Laser;
    protected RaycastHit obj;
    protected float Shoottimer;
    protected bool lazergate;


    public float MaxLength;
    public float MainTextureLength = 1f;
    public float NoiseTextureLength = 1f;
    private Vector4 Length = new Vector4(1,1,1,1);

    protected override void Start()
    {
        base.Start();
        Laser = GetComponent<LineRenderer>();
    }
    protected void LockTarget()
    {
        this.TarGet.parent.position = Vector3.Lerp(this.TarGet.position,PlayerController.Instance.transform.position,Time.deltaTime * 1f * 0.5f) ;
    }
    protected void lazering()
    { 
        Laser.positionCount = 2;
        Vector3 Origin = this.transform.position;
        Vector3 Direction = (TarGet.transform.position - Origin);
        Laser.SetPosition(0,Origin);
        if(Physics.Raycast(Origin,Direction,out obj,Direction.normalized.magnitude * 50f,LazerCanHit))
        {
            Laser.SetPosition(1,obj.point);
            if(lazergate)
            {
                lazergate = false;
                this.DamePoint =  EnemieActSpawner.Instance.Spawn(EnemieActSpawner.ActEnum.LazerHit.ToString(),obj.point,Quaternion.identity);
                DamePoint.GetComponentInChildren<DespawnLazerHit>().Obj = this.transform.parent;
            }
            DamePoint.position = obj.point;
       }
    }
    protected void TimeController()
    {
        if(timer <= timerate) 
        {
            Shoottimer = 0;
            this.Laser.positionCount = 0 ;
            if(DamePoint != null) EnemieActSpawner.Instance.DeSpawnToPool(this.DamePoint);
        } 
        if(gate) timer += Time.deltaTime * 1f;
        if(timer > timerate)
        {
            Shoottimer += Time.deltaTime * 1f;
        }   
        if(Shoottimer >= 0 && Shoottimer < PreShootTime)
        {
            lazergate = true;
            this.LockTarget();
        }
        else if(Shoottimer >= PreShootTime && Shoottimer < PreShootTime + ShootingTime)
        {
            this.lazering();
        }
        else 
        {
            gate = false;
            timer = 0;
        }
    }
    protected override void Action()
    {
        Laser.material.SetTextureScale("_MainTex", new Vector2(Length[0], Length[1]));                    
        this.TimeController();
        base.Action();
    }    
}
