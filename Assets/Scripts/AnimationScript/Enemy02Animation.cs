using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Animation : MyBehaviour
{    
    public EnemieCtrl enemieCtrl;
    [SerializeField] protected Animator Animator;
    protected int State;
    [SerializeField] protected int RUN = 1,
    ATTACK = 2,
    DIE = 3;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemiesCtrl();
    }
    protected void LoadEnemiesCtrl()
    {
        if(enemieCtrl != null) return;
        enemieCtrl = GetComponentInParent<EnemieCtrl>();
    }
    protected void FixedUpdate()
    {
        if(enemieCtrl.TrackPlayer.Tracking) State = RUN;
        else State = ATTACK;
        if(enemieCtrl.EnemiesReciver.CurrentHp <= 0) State = DIE;
        Animator.SetInteger(StringConts.Enemies02Anim,State);
    }

}
