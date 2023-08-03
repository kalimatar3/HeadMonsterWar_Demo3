using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies01Animation : MyBehaviour
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
    protected  void Update()
    {
        if(enemieCtrl.TrackPlayer.Tracking) State = RUN;
        else State = ATTACK;
        if(enemieCtrl.EnemiesReciver.CurrentHp <= 0) State = DIE;
        Animator.SetInteger(StringConts.Enemies01Anim,State);
    }
}
