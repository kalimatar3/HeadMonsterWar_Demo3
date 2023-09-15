using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamePanelSpawner : Spawner
{
    public string TakeDamePanel = "TakeDamePanel";
    public RectTransform TakeDamepanelTrans;
    protected static TakeDamePanelSpawner instance;
    public static TakeDamePanelSpawner Instance { get => instance ;}
    public  Vector3 BasePos;
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
}
