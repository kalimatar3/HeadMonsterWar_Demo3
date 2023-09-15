using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MyBehaviour
{
    protected static CameraManager instance;
    public static CameraManager Instance { get => instance;}
    [SerializeField] protected CameraFollow cameraFollow;
    public CameraFollow CameraFollow => cameraFollow;
    protected override void Awake()
    {
        base.Awake();
        if(instance != null && instance != this)
        {
            Destroy(this);
            Debug.LogWarning(this.gameObject + "Existed");
        }
        else instance = this;
    }
    protected void LoadCameraFollow()
    {
        this.cameraFollow = GetComponentInChildren<CameraFollow>();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCameraFollow();
    }
}
