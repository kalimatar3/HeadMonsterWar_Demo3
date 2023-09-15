using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : PlayerAct
{
    protected GunCtrl gunCtrl;
    [Header("Fire")]
    [SerializeField] protected Transform Guntip;
    [SerializeField] protected float FireRate,Dame;
    [Header(" Ammo ")]
    [SerializeField] public float Reloadtime;
    [SerializeField] public float MaxAmmo,CurrentAmmo;
    public float reloadtimer,firetimer;
    [SerializeField] protected string Bulletname,ThisExplosionFireName,ThisExplosionHitName;
    [SerializeField] public Transform ReloadPanel;
    protected Transform ThisBullet;
    protected float BulletSpeed,Range;
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
    }
    protected override void Start()
    {
        base.Start();
        this.reborn();
    }
    protected  void OnEnable()
    {
        this.reborn();
    }
    protected void LoadGunCtrl()
    {
        if(gunCtrl != null) return;
        gunCtrl = this.transform.parent.GetComponentInParent<GunCtrl>();
    }
    public virtual void reborn()
    {
        this.Getbulletdata();
        this.ReloadPanel.gameObject.SetActive(false);
        this.CurrentAmmo = this.MaxAmmo;
    }
    protected virtual void Reload()
    {
        if(Reloadgate)
        {
            if(reloadtimer > 0) reloadtimer -= Time.deltaTime * 1f;
            else
            {
                this.ReloadPanel.gameObject.SetActive(false);
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
            this.ReloadPanel.gameObject.SetActive(true);
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
                this.BulletSpeed = gunCtrl.ListGunSO[i].bulletSpeed;
                this.Range = gunData.Range;
                FireRate  = gunData.Firerate;
                MaxAmmo = gunData.MaxAmmo;
                Reloadtime = gunData.Reloadtime;
                this.Dame = gunData.Dame;
            }
        } 
        if(CurrentAmmo > MaxAmmo) CurrentAmmo = MaxAmmo;
  }
    protected virtual void Shot()
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
                    this.ThroughBulet();
                    CurrentAmmo --;
                }
            }
            else 
            {
                firetimer = 0;
            }
        }
    }
    protected virtual void ThroughBulet()
    {
        ThisBullet =  BulletSpawner.Instance.Spawn(Bulletname,Guntip.position,Guntip.rotation);
        Transform thisFireEffect = EffectSpawner.Instance.Spawn(ThisExplosionFireName,Guntip.position,Guntip.rotation);
        thisFireEffect.GetComponentInChildren<followObj>().Obj = this.Guntip;
        DealToEnemies Dealer = ThisBullet.GetComponentInChildren<DealToEnemies>();
        if(Dealer == null) Debug.LogWarning(ThisBullet + "dont have DealtoEnnemy");
        ThisBullet.GetComponentInChildren<BulletDeSpawn>().DistanceToDepSpawn = this.Range;
        ThisBullet.GetComponentInChildren<Through>().speed = this.BulletSpeed;
        Dealer.CanThroughObj = this.CanThroughObj;
        Dealer.dealnumber = this.Dame;
        Dealer.dealnumber = ThisBullet.GetComponentInChildren<DealToEnemies>().dealnumber*(1 + ExtraDame);
        Dealer.ExplosionHitName = ThisExplosionHitName;
        ThisBullet.GetComponentInChildren<DealToEnvironments>().ExplosionHitName = ThisExplosionHitName;
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
        this.Getbulletdata();
        this.AutoReload();
        this.Reload();
        base.Action();
    }
    protected override void Doing()
    {
        base.Doing();
       this.Shot();
    }
    protected override bool CanDo()
    {
        if(CurrentAmmo <=0) return false;
        return true;
    }
}
