using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCtrl : MyBehaviour
{
    protected static GunCtrl instance;
    public static GunCtrl Instance { get => instance ;}
    public List<Transform> ListGuns;
    public List<GunsSO> ListGunSO;
    [SerializeField] protected Shooting shooting;
    public Shooting Shooting { get => shooting;}
    [SerializeField] protected Transform ReloadPanel;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShooting();
        this.LoadPrefabs();
        this.LoadGunSO();
    }
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
    protected virtual void LoadPrefabs()
    {
        if(ListGuns.Count > 0)  return;
        Transform Prefabs = transform.Find("Prefabs");
        foreach(Transform Pre in Prefabs)
        {
            this.ListGuns.Add(Pre);
        }
    }
    protected virtual void LoadShooting()
    {
        if(shooting != null) return;
        shooting = GetComponentInChildren<Shooting>();
        shooting.ReloadPanel = ReloadPanel;
    }
    protected virtual void LoadGunSO()
    {
        if(ListGunSO.Count > 0) return;
        Transform Pre = transform.Find("Prefabs");
        if(Pre == null) return;
        foreach(Transform GunName in Pre)
        {
        string rePath = "Guns/" + GunName.name;
        ListGunSO.Add(Resources.Load<GunsSO>(rePath));
        Debug.Log( this.transform.name  + " LoadSO");
        }
    }
    protected void ActiveGunbyName(string name)
    {
        foreach(Transform gun in ListGuns)
        {
            if(gun.name == name)
            {
                gun.gameObject.SetActive(true);
                this.shooting = gun.GetComponentInChildren<Shooting>();
            }
            else gun.gameObject.SetActive(false);

        }
    }
    public void ActiveGun()
    {
       this.ActiveGunbyName(DataManager.Instance.CurrentGunName);
    }
    protected IEnumerator ActiveGunDelay()
    {
        yield return new WaitUntil(predicate : ()=>{ if(DataManager.Instance == null) return false;
        return true;
        });
        this.ActiveGun();
    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(this.ActiveGunDelay());
    }

}
