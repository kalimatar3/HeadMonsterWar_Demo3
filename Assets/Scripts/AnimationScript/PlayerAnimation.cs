using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MyBehaviour
{
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected Animator PlayerTopAnimator,PlayerUnderAnimator;
    [SerializeField] protected bool IsDeadAnim,facing;
    protected float MoveAnim,Reload;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
    }
    protected void LoadPlayerCtrl()
    {
        if(playerController != null) return;
        playerController = GetComponentInParent<PlayerController>();
    }
    protected void Update()
    {
        this.Moving();
        this.Attack();
        this.IsDead();
        this.Reloading();
        this.FistCamScense();
    }
    protected void FistCamScense()
    {
        PlayerTopAnimator.SetBool(StringConts.FistCamScense,DataManager.Instance.FistCamMove);
        PlayerUnderAnimator.SetBool(StringConts.FistCamScense,DataManager.Instance.FistCamMove);  
    }
    protected void Reloading()
    {
        if(GunCtrl.Instance.Shooting == null) return;
        PlayerTopAnimator.SetFloat(StringConts.PlayerReload,playerController.GunCtrl.Shooting.reloadtimer);
        PlayerUnderAnimator.SetFloat(StringConts.PlayerReload,playerController.GunCtrl.Shooting.reloadtimer);
    }
    protected void IsDead()
    {
        if(playerController.PlayerReciver.CurrentHp <= 0)
        {
            IsDeadAnim = true;
        }
        else
        {
            IsDeadAnim = false;
        }
        PlayerTopAnimator.SetBool(StringConts.PlayerIsDead,IsDeadAnim);
        PlayerUnderAnimator.SetBool(StringConts.PlayerIsDead,IsDeadAnim);
    }
    protected void Moving()
    {
        if( Vector3.Angle(new Vector3(InputManager.Instance.MovingJoystick.Horizontal,0,InputManager.Instance.MovingJoystick.Vertical),new Vector3(InputManager.Instance.Shootingstick.Horizontal,0,InputManager.Instance.Shootingstick.Vertical)) >= 120)
        {
            facing = false;
        }
        else facing = true;
        this.MoveAnim = playerController.PlayerMoving.Move.magnitude;
        PlayerTopAnimator.SetFloat(StringConts.PlayerMoveAnim,MoveAnim);
        PlayerUnderAnimator.SetFloat(StringConts.PlayerMoveAnim,MoveAnim);
        PlayerUnderAnimator.SetBool(StringConts.PlayerFacingAnim,facing);              
    }
    protected void Attack()
    {
        Vector3 GunDirection = new Vector3 (InputManager.Instance.Shootingstick.Horizontal,0 ,InputManager.Instance.Shootingstick.Vertical);
        PlayerTopAnimator.SetFloat(StringConts.PlayerFireAnim,GunDirection.magnitude);
        PlayerUnderAnimator.SetFloat(StringConts.PlayerFireAnim,GunDirection.magnitude);
    }
}
