using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperShooting : Shooting
{
    [SerializeField] protected LineRenderer line;
    [SerializeField] protected bool Canfire;
    [SerializeField] protected float BulletDameLoad;
    protected float DameBase;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLine();
    } 
    protected override void Start()
    {
        base.Start();
        this.DameBase = Dame;
    } 
    protected void LoadLine()
    {
        line = GetComponent<LineRenderer>();
    }
    protected override void Shot()
    {
        if(this.Reloadgate) return;
        float ShotLoad = firetimer * Range/FireRate;
        shootingVector = new Vector3(InputManager.Instance.Shootingstick.Horizontal,0,InputManager.Instance.Shootingstick.Vertical);
        if(shootingVector.magnitude > 0)
        {            
            firetimer += Time.deltaTime * 1f;
            BulletDameLoad = firetimer/FireRate * 100f;
            line.positionCount = 2;
            line.SetPosition(0,Guntip.position);
            line.SetPosition(1,Guntip.position + (Guntip.forward *firetimer * Range/FireRate));
            if(firetimer >= FireRate*30/100)   Canfire = true;
        }
        else 
        {
            if(Canfire)
            {
                Canfire = false;
                this.ThroughBulet();
                CurrentAmmo --;
            }
            line.positionCount = 0;
            firetimer = 0;
        }
    }
    protected override void ThroughBulet()
    {
        this.Dame = DameBase * BulletDameLoad/100f;
        base.ThroughBulet();
    } 
}

