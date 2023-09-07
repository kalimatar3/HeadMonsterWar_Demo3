using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnderRotation : MonoBehaviour
{
    [SerializeField] protected Vector3 Tip;
    protected void RotateUnder()
    {
        Tip = new Vector3(InputManager.Instance.MovingJoystick.Horizontal,0,InputManager.Instance.MovingJoystick.Vertical);
        if(Tip.magnitude > 0)
        {
            this.transform.forward = Vector3.Lerp(this.transform.forward,Tip,Time.deltaTime *1f *100f);
            if(Vector3.Angle(new Vector3(InputManager.Instance.MovingJoystick.Horizontal,0,InputManager.Instance.MovingJoystick.Vertical),new Vector3(InputManager.Instance.Shootingstick.Horizontal,0,InputManager.Instance.Shootingstick.Vertical)) >= 120)
            {
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation,Quaternion.LookRotation(-Tip),Time.deltaTime * 1f * 100f);
            }
        }
    }
    protected void FixedUpdate()
    {
        this.RotateUnder();
    }
}
