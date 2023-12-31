using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerTopRotation : MyBehaviour
{
    [SerializeField] protected Vector3 Tip;
    [SerializeField] protected float Angler;
    [SerializeField] protected PlayerUnderRotation playerUnderRotation;
    protected Quaternion cacheRos;
    protected float timer;
    protected void RotateTop()
    {
        if(new Vector3 (InputManager.Instance.Shootingstick.Horizontal,0,InputManager.Instance.Shootingstick.Vertical).magnitude > 0)
        {
            timer = 0 ;
            cacheRos = this.transform.rotation;
            Tip = new Vector3(InputManager.Instance.Shootingstick.Horizontal,0,InputManager.Instance.Shootingstick.Vertical / Mathf.Cos((Mathf.PI)/6));
            Angler = Vector3.Angle(this.transform.forward,playerUnderRotation.transform.forward);
            this.transform.rotation = Quaternion.LookRotation(Tip);
            if(Angler >= 60 && new Vector3 (InputManager.Instance.MovingJoystick.Horizontal,0,InputManager.Instance.MovingJoystick.Vertical).magnitude <= 0) 
            {
                playerUnderRotation.transform.rotation = Quaternion.Lerp(playerUnderRotation.transform.rotation,Quaternion.LookRotation(Tip),Time.deltaTime * 1f * 500f );
            }
        }
        else
        {
            this.transform.rotation =  cacheRos;
            timer += Time.deltaTime * 1f;
            if(timer >= Time.deltaTime)
            {
                this.transform.rotation =  Quaternion.Lerp(this.transform.rotation,Quaternion.LookRotation(playerUnderRotation.transform.forward),timer * 1f);
                cacheRos = Quaternion.LookRotation(playerUnderRotation.transform.forward);
                timer = 0;
            }
        }
    }
    protected void FixedUpdate()
    {
        this.RotateTop();
    }
}
