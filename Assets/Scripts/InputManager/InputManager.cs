using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MyBehaviour
{
    protected static InputManager instance;
    public static InputManager Instance { get => instance;}
    public Joystick MovingJoystick;
    public Joystick Shootingstick;
    [HideInInspector] public Vector3 KeyboardMoving;
    protected Vector3 worldPosition;
    protected override void Awake()
    {
        base.Awake();
        if(instance != null && instance != this)
        {
            Destroy(this);
            Debug.LogWarning(this.gameObject + "Does Existed");
        }
        else instance = this;
    }
    protected void KeyboardMove()
    {
        this.KeyboardMoving = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")) * Time.deltaTime * 1f;
    }
    protected void Update()
    {
        this.KeyboardMove();
    }
}
