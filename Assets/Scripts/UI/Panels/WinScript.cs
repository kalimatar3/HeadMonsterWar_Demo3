using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class WinScript : MyBehaviour
{
    [SerializeField] protected Transform ContinueButton;
    protected void OnEnable()
    {
        SoundSpawner.Instance.Spawn(CONSTSoundsName.Win,Vector3.zero,Quaternion.identity);
        PanelCtrl.Instance.HirePanel("GameplayPanel");
    } 
}
