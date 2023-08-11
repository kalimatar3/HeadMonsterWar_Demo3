using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMoving : MyBehaviour
{
[SerializeField] protected Rigidbody Mybody; 
[SerializeField] protected float DefaultPlayerMovingSpeed;
[SerializeField] protected float FireMoveRate;
[SerializeField] protected PlayerController playerController;
[HideInInspector] public float BoostValue,BoostTime;
[SerializeField] protected float CurrentSpeed;
protected float Timer,ExtraSpeed,soundtimer;
protected bool LR;
public Vector3 Move; 
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
        this.Mybody = GetComponentInParent<Rigidbody>();
        if(Mybody == null) Debug.LogWarning("Can't Found Rigidbody");
    }
    protected virtual void Moving()
    {
        CurrentSpeed = DefaultPlayerMovingSpeed   * ( 1 + ExtraSpeed) - FireMoveRate*DefaultPlayerMovingSpeed/100f;
        this.Move = new Vector3 (InputManager.Instance.MovingJoystick.Horizontal * CurrentSpeed, this.Mybody.velocity.y , InputManager.Instance.MovingJoystick.Vertical * CurrentSpeed);
        this.Mybody.velocity = Move;
        Vector3 ShootJoyVec = new Vector3 (InputManager.Instance.Shootingstick.Horizontal,0, InputManager.Instance.Shootingstick.Vertical);
        if(ShootJoyVec.magnitude > 0)
        {
            FireMoveRate = 30;
        }
        else 
        {
            FireMoveRate = 0;
        }
        if(!InputManager.Instance.MovingJoystick.gameObject.activeInHierarchy) 
        {
            this.Mybody.velocity = Vector3.zero;
        }
    }
    protected void StepSound()
    {
        soundtimer += Time.deltaTime * 1f;
        if(Move.magnitude >=1 && soundtimer > 4 *1 /CurrentSpeed)
        {
            soundtimer =0;
            if(!LR) 
            {
                LR = true;
                SoundSpawner.Instance.Spawn(CONSTSoundsName.PlayerMoving1,Vector3.zero,Quaternion.identity);
            }
            else
            {
                LR = false;
                SoundSpawner.Instance.Spawn(CONSTSoundsName.PlayerMoving2,Vector3.zero,Quaternion.identity);
            }
        } 
    }
    protected void LoadPlayerCtrl()
    {
        if(playerController != null) return;
        playerController = GetComponentInParent<PlayerController>();
        if(playerController == null ) Debug.LogWarning(this.transform + "can find playerCtrl");
    }
    protected void FixedUpdate()
    {
        this.StepSound();
        this.Moving();
        this.BoostSpeed(BoostValue,BoostTime);
    }
    protected void BoostSpeed(float Value,float time)
    {
        if(BuffManager.Instance.CurrentBuff == null) return;
        if(BuffManager.Instance.CurrentBuff.name == "SpeedUp")
        {
            this.Timer += Time.deltaTime * 1f;
            if(Timer <= time) ExtraSpeed = Value/100f; 
            else
            {
                this.Timer  = 0 ;
                BuffManager.Instance.CurrentBuff.name = null;
                BuffManager.Instance.CurrentBuffEffect = null;
            }
        }
        else
        {
            ExtraSpeed = 0;
        } 
    }
}
