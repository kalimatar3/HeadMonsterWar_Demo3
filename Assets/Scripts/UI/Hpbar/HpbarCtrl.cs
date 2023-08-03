using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpbarCtrl : MyBehaviour
{
    protected static HpbarCtrl instance;
    public static HpbarCtrl Instance { get => instance;}

    [SerializeField] protected followObj followObj;
    public followObj FollowObj { get => followObj;}
    [SerializeField] protected DeSpawnHpbar deSpawnHpbar;
    public DeSpawnHpbar DeSpawnHpbar { get => DeSpawnHpbar ;}
    protected override void Awake()
    {
        base.Awake();
        if(instance != null && instance != this)
        {
            Destroy(this);
            Debug.LogWarning(this.gameObject + "Does Existed");
        }
        else instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadfollowObj();
        this.LoadDeSpawnHpbar();
    }
    protected virtual void LoadfollowObj()
    {
        if(followObj != null) return;
        followObj = GetComponentInChildren<followObj>();
    }
    protected virtual  void LoadDeSpawnHpbar()
    {
        if(DeSpawnHpbar!= null) return;
        deSpawnHpbar = GetComponentInChildren<DeSpawnHpbar>();
    }
}
