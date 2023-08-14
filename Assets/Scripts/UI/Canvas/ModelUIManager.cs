using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelUIManager : MyBehaviour
{
    protected static ModelUIManager instance;
    public static ModelUIManager Instance { get => instance ;}
    [SerializeField] public List<Transform> ListModels,ListGuns;
    [SerializeField] protected RectTransform Models,Guns;
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
        this.LoadListGun();
    }
    protected void OnEnable()
    {
        this.StartCoroutine(this.DelayActive());
    }
    protected IEnumerator DelayActive()
    {
        yield return new WaitUntil( predicate: ()=>
        {
            if(DataManager.Instance.CurrentModelName == null) return false;
            else return true;
        });
        this.ActiveModel(DataManager.Instance.CurrentModelName); 
        this.ActiveGun(DataManager.Instance.CurrentGunName);
    }
    protected void LoadListModel()
    {
        if(ListModels.Count > 0 ) return;
        foreach(Transform element in this.Models)
        {
            ListModels.Add(element);
        }
    }
        protected void LoadListGun()
    {
        if(ListGuns.Count > 0 ) return;
        foreach(Transform element in this.Guns)
        {
            ListGuns.Add(element);
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
    public void ActiveGun(string GunName)
    {
        foreach(Transform element in ListGuns)
        {
            element.gameObject.SetActive(false);
            if(GunName == element.name)
            {
                element.gameObject.SetActive(true);
            }
        }
    }

}
