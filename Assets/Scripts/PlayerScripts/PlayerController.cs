using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MyBehaviour
{
    protected static PlayerController instance;
    public static PlayerController Instance { get => instance;}
    [SerializeField]  protected PlayerMoving playerMoving;
    public PlayerMoving PlayerMoving { get => playerMoving ;}
    [SerializeField] protected GunCtrl gunCtrl;
    public GunCtrl GunCtrl {get => gunCtrl;}
    [SerializeField]  protected PlayerReciver playerReciver;
    public PlayerReciver PlayerReciver {get => playerReciver;}
    public PlayerSO playerSO;
    [SerializeField]  protected PlayerAnimation playerAnimation;
    public PlayerAnimation PlayerAnimation {get => playerAnimation;}

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadGunCtrl();
        this.LoadMoving();
        this.LoadReciver();
        this.LoadPlayerSO();
        this.LoadPlayerAniamtion();
    }
    protected void LoadGunCtrl()
    {
        if(gunCtrl != null) return;
        gunCtrl = GetComponentInChildren<GunCtrl>();
    }
    protected void LoadMoving()
    {
        if(playerMoving != null) return;
        playerMoving = GetComponentInChildren<PlayerMoving>();
    }
    protected void LoadPlayerAniamtion()
    {
        if(playerAnimation != null) return;
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
    }

    protected void LoadReciver()
    {
        if(playerReciver != null) return;
        playerReciver = GetComponentInChildren<PlayerReciver>();
    } 
    protected virtual void LoadPlayerSO()
    {
        if(playerSO != null) return;
        string rePath = "Player/" + transform.name;
        this.playerSO = Resources.Load<PlayerSO>(rePath);
        Debug.LogWarning( this.transform.name  + " LoadSO");

    }
    protected override void Awake()
    {
        base.Awake();
        if(instance != null && instance != this)
        {
            Destroy(this);
            Debug.LogWarning(this.gameObject + "Existed");
        }
        else instance = this;
    }
}
