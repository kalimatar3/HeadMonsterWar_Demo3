using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : MyBehaviour
{
    protected static ModelManager instance;
    public static ModelManager Instance { get => instance ;}
    public List<Transform> ListModel;
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }

    protected override void LoadComponents()
    {
        this.LoadListModel();
        base.LoadComponents();
    }
    protected virtual void LoadListModel()
    {
        Transform Prefabs = transform.Find("Prefabs");
        if(Prefabs == null) Debug.LogWarning( this.transform + "Can't Found Prefabs");
        if(ListModel.Count > 0) return;
        foreach(Transform pre in Prefabs)
        {
            ListModel.Add(pre);
        }
    }
    protected void ActiveModelbyname(string name)
    {
        foreach(Transform model in ListModel)
        {
            if(model.name == name)
            {
                model.gameObject.SetActive(true);
            }
            else   model.gameObject.SetActive(false);
        }
    }
    public void ActiveModel()
    {
       this.ActiveModelbyname(DataManager.Instance.CurrentModelName);
    }
    protected IEnumerator ActiveModelDelay()
    {
        yield return new WaitUntil(predicate : ()=>{ if(DataManager.Instance == null) return false;
        return true;
        });
        this.ActiveModel();
    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(this.ActiveModelDelay());
    }
}
