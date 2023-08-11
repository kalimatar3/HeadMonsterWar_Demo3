using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnderRotation : MonoBehaviour
{
    [SerializeField] protected Vector3 Tip;
    protected void RotateUnder()
    {
        if(new Vector3 (InputManager.Instance.MovingJoystick.Horizontal,0,InputManager.Instance.MovingJoystick.Vertical).magnitude > 0)
        {
            Tip = new Vector3(InputManager.Instance.MovingJoystick.Horizontal,0,InputManager.Instance.MovingJoystick.Vertical);
            PlayerController.Instance.transform.rotation = Quaternion.Lerp(PlayerController.Instance.transform.rotation,Quaternion.LookRotation(Tip),Time.deltaTime * 1f * 10f);
           if(Vector3.Angle(new Vector3(InputManager.Instance.MovingJoystick.Horizontal,0,InputManager.Instance.MovingJoystick.Vertical),new Vector3(InputManager.Instance.Shootingstick.Horizontal,0,InputManager.Instance.Shootingstick.Vertical)) >= 120)
            {
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation,Quaternion.LookRotation(-Tip),Time.deltaTime * 1f * 10f);
            }
            else    
            {
                this.transform.rotation = new Quaternion(0,0,0,0);
            }
        }
        else
        {
            this.transform.rotation = new Quaternion(0,0,0,0);
        }
    }
    protected void FixedUpdate()
    {
        this.RotateUnder();
    }
}
