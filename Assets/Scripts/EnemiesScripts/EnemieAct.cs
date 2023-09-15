using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieAct : ActbyDis
{
    [SerializeField]  protected float timerate;
    [SerializeField] protected float timer,FistTimeAttack;
    protected EnemieCtrl EnemieCtrl;
    protected EnemiesSO enemiesSO;
    public bool gate;
    protected void LoadEnemieSO()
    {
        if(enemiesSO != null) return;
        enemiesSO = GetComponentInParent<EnemiesSO>();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemieCtrl();
    }
    protected override void Action()
    {
        if(this.CanDo() && !this.gate)   this.gate = true;
        if(!this.gate)   
        {
            timer = FistTimeAttack;
            return;
        }
        this.Doing();
    }
    protected void LoadEnemieCtrl()
    {
        if(EnemieCtrl != null) return;
        EnemieCtrl = this.transform.parent.GetComponent<EnemieCtrl>();
    }
    protected override void Start()
    {
        if(Oritrans != null) return;
        this.Oritrans = PlayerController.Instance.transform;
    }
    protected virtual void DontMove()
    {
        EnemieCtrl.TrackPlayer.gameObject.SetActive(gate);
    }
}
