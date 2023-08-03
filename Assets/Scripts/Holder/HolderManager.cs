using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderManager : MyBehaviour
{
    [SerializeField] protected List<Transform> ListHolder;
    protected static HolderManager instance;
    public static HolderManager Instance { get => instance ;}
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadListHolder();
    }
    protected void LoadListHolder()
    {
        if(ListHolder.Count > 0) return;
        foreach(Transform element in this.transform)
        {
            ListHolder.Add(element);
        }
    }
    public void DeActiveHolder( string Name)
    {
        foreach(Transform element in ListHolder)
        {
            if(element.name == Name) element.transform.gameObject.SetActive(false);
        }
    }
    public void ActiveHolder( string Name)
    {
        foreach(Transform element in ListHolder)
        {
            if(element.name == Name) element.transform.gameObject.SetActive(true);
        }
    }

}
