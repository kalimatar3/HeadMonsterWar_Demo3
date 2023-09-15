using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MyBehaviour
{
    protected static CameraCtrl instance;
    public static CameraCtrl Instance { get => instance;}
    [SerializeField] protected CameraFollow cameraFollow;
    public CameraFollow CameraFollow => cameraFollow;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadFollow();
    }
    protected virtual void LoadFollow()
    {
        cameraFollow = GetComponentInChildren<CameraFollow>();
        if(cameraFollow== null) Debug.LogWarning(this.transform.parent + "Can't found cammerafolllow");
    }
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

}
