using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerTopRotation : MyBehaviour
{
    [SerializeField] protected Vector3 Tip;
    [SerializeField] protected float Angler;
    protected Quaternion cacheRos;
    protected float timer;
    protected void RotateTop()
    {
        if(new Vector3 (InputManager.Instance.Shootingstick.Horizontal,0,InputManager.Instance.Shootingstick.Vertical).magnitude > 0)
        {
            timer = 0 ;
            cacheRos = this.transform.rotation;
            Tip = new Vector3(InputManager.Instance.Shootingstick.Horizontal,0,InputManager.Instance.Shootingstick.Vertical * 2);
            Angler = Vector3.Angle(this.transform.forward,PlayerController.Instance.transform.forward);
            this.transform.rotation = Quaternion.LookRotation(Tip);
            if(Angler >= 60 && new Vector3 (InputManager.Instance.MovingJoystick.Horizontal,0,InputManager.Instance.MovingJoystick.Vertical).magnitude <= 0) 
            {
                PlayerController.Instance.transform.rotation = Quaternion.Lerp(PlayerController.Instance.transform.rotation,Quaternion.LookRotation(Tip),Time.deltaTime * 1f * 20f );
            }
        }
        else
        {
            this.transform.rotation =  cacheRos;
            timer += Time.deltaTime * 1f;
            if(timer >= Time.deltaTime)
            {
                this.transform.rotation =  Quaternion.Lerp(this.transform.rotation,Quaternion.LookRotation(PlayerController.Instance.transform.forward),timer * 10f );
                cacheRos = Quaternion.LookRotation(PlayerController.Instance.transform.forward);
                timer = 0;
            }
        }
    }
    protected void FixedUpdate()
    {
        this.RotateTop();
    }
}
