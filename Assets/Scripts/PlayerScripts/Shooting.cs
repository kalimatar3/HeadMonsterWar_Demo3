using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : PlayerAct
{
    [Header("Fire")]
    [SerializeField] protected Transform Guntip;
    [SerializeField] protected float FireRate,Dame;
    [Header(" Ammo ")]
    [SerializeField] protected float Reloadtime;
    [SerializeField] public float MaxAmmo,CurrentAmmo;
    [HideInInspector] public float reloadtimer,firetimer;
    protected GunCtrl gunCtrl;
    protected string Bulletname,ThisExplosionFireName,ThisExplosionHitName;
    protected Transform ThisBullet;
    protected bool CanThroughObj;
    protected bool Reloadgate;
    // BUFF
    public float ExtraDame,Timer,BoostTime,BoostValue;
    public bool CanDameUp;
    public Vector3 shootingVector;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadGunCtrl();
        this.reborn();
    }
    protected void LoadGunCtrl()
    {
        if(gunCtrl != null) return;
        gunCtrl = GetComponentInParent<GunCtrl>();
    }
    public virtual void reborn()
    {
        this.Getbulletdata();
        this.CurrentAmmo = this.MaxAmmo;
    }
    protected virtual void Reload()
    {
        if(Reloadgate)
        {
            if(reloadtimer > 0) reloadtimer -= Time.deltaTime * 1f;
            else
            {
                Reloadgate = false; 
                reloadtimer = 0;
                CurrentAmmo = MaxAmmo;
           }
        }
    }
    public void Reloading()
    {
        if(CurrentAmmo < MaxAmmo) 
        {
            SoundSpawner.Instance.Spawn(CONSTSoundsName.Reload,Vector3.zero,Quaternion.identity);
            reloadtimer = Reloadtime;
            Reloadgate = true;
            Debug.Log(this.transform.parent + "Reloading");
        }
    }
    protected virtual void AutoReload()
    {
        if(CurrentAmmo <= 0 && Reloadgate == false ) 
        {
            this.Reloading();
        }
    }
    protected virtual void Getbulletdata()
    {
        for( int i = 0; i < gunCtrl.ListGuns.Count ;i ++)
        {
            if(gunCtrl.ListGuns[i].gameObject.activeInHierarchy)
            {
                ClassData.GunData gunData = gunCtrl.ListGunSO[i].GunUpgrade[DataManager.Instance.GetUpgradenumberfromUGAD(DataManager.Instance.CurrentGunName)];
                Bulletname =  gunCtrl.ListGunSO[i].Bulletname;
                ThisExplosionFireName = gunCtrl.ListGunSO[i].FireExplosionName;
                ThisExplosionHitName = gunCtrl.ListGunSO[i].HitExplosionName;
                CanThroughObj = gunCtrl.ListGunSO[i].CanThroughObj;
                FireRate  = gunData.Firerate;
                MaxAmmo = gunData.MaxAmmo;
                Reloadtime = gunData.Reloadtime;
                this.Dame = gunData.Dame;
            }
        } 
        if(CurrentAmmo > MaxAmmo) CurrentAmmo = MaxAmmo;
  }
    protected void shotting()
    {
        if(this.Reloadgate) return;
        if(InputManager.Instance.Shootingstick.gameObject.activeInHierarchy)
        {
            shootingVector = new Vector3(InputManager.Instance.Shootingstick.Horizontal,0,InputManager.Instance.Shootingstick.Vertical);
            if(shootingVector.magnitude > 0)
            {
                firetimer += Time.deltaTime * 1f;
                if(firetimer > FireRate)
                {
                    firetimer = 0;
                    ThisBullet =  BulletSpawner.Instance.Spawn(Bulletname,Guntip.position,Guntip.rotation);
                    EffectSpawner.Instance.Spawn(ThisExplosionFireName,Guntip.position,Guntip.rotation);
                    ThisBullet.GetComponentInChildren<DealToEnemies>().CanThroughObj = this.CanThroughObj;
                    ThisBullet.GetComponentInChildren<DealToEnemies>().dealnumber = this.Dame;
                    ThisBullet.GetComponentInChildren<DealToEnemies>().dealnumber = ThisBullet.GetComponentInChildren<DealToEnemies>().dealnumber*(1 + ExtraDame);
                    ThisBullet.GetComponentInChildren<DealToEnemies>().ExplosionHitName = ThisExplosionHitName;
                    ThisBullet.GetComponentInChildren<DealToEnvironments>().ExplosionHitName = ThisExplosionHitName;
                    CurrentAmmo --;
                }
            }
        }
    }
    public void RestoreGun()
    {
        shootingVector = Vector3.zero;
    }
    protected void DameUp(float Value,float time)
    {
        if(BuffManager.Instance.CurrentBuff == null) return;
        if(BuffManager.Instance.CurrentBuff.name == "DameUp")
        {
            this.Timer += Time.deltaTime  * 1f;
            if(Timer <= time) ExtraDame = Value/100f; 
            else
            {
                this.Timer  = 0 ;
                BuffManager.Instance.CurrentBuff = null;
            }
        }
        else 
        {
            this.Timer = 0;
            ExtraDame = 0;
        }
    }
    protected override void Action()
    {
        this.DameUp(BoostValue,BoostTime);
        //this.Getbulletdata();
        this.AutoReload();
        this.Reload();
        base.Action();
    }
    protected override void Doing()
    {
        base.Doing();
       // this.shotting();
    }
    protected override bool CanDo()
    {
        if(CurrentAmmo <=0) return false;
        return true;
    }
}
