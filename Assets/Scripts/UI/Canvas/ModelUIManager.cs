using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelUIManager : MyBehaviour
{
    protected static ModelUIManager instance;
    public static ModelUIManager Instance { get => instance ;}
    [SerializeField] public List<Transform> ListModels;
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadListModel();
    }
    protected override void Start()
    {
        base.Start();
        this.StartCoroutine(this.DelayActiveModel());
    }
    protected IEnumerator DelayActiveModel()
    {
        yield return new WaitUntil( predicate: ()=>
        {
            if(DataManager.Instance.CurrentModelName == null) return false;
            else return true;
        });
        this.ActiveModel(DataManager.Instance.CurrentModelName); 
    }
    protected void LoadListModel()
    {
        if(ListModels.Count > 0 ) return;
        foreach(Transform element in this.transform)
        {
            ListModels.Add(element);
        }
    }
    public void ActiveModel(string Modelname)
    {
        foreach(Transform element in ListModels)
        {
            element.gameObject.SetActive(false);
            if(Modelname == element.name)
            {
                element.gameObject.SetActive(true);
            }
        }
    }
}
