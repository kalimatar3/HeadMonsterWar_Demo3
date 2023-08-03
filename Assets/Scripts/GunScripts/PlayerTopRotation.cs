using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopRotation : MyBehaviour
{
    [SerializeField] protected Vector3 Tip;
    [SerializeField] protected float Angler;
    protected void RotateTop()
    {
        if(new Vector3 (InputManager.Instance.Shootingstick.Horizontal,0,InputManager.Instance.Shootingstick.Vertical).magnitude > 0)
        {
            Tip = new Vector3(InputManager.Instance.Shootingstick.Horizontal,0,InputManager.Instance.Shootingstick.Vertical);
            Angler = Vector3.Angle(this.transform.forward,PlayerController.Instance.transform.forward);
            this.transform.rotation = Quaternion.LookRotation(Tip);
            if(Angler >= 75) 
            {
                PlayerController.Instance.transform.rotation = Quaternion.Lerp(PlayerController.Instance.transform.rotation,Quaternion.LookRotation(Tip),Time.deltaTime * 1f * 10f);
            }
        }
        else
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation,Quaternion.LookRotation(PlayerController.Instance.transform.forward), Time.deltaTime * 1f * 10f);
        }
    }
    protected void FixedUpdate()
    {
        this.RotateTop();
    }
}
