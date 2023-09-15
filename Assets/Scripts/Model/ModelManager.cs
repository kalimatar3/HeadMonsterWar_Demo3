using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : MyBehaviour
{
    protected static ModelManager instance;
    public static ModelManager Instance { get => instance ;}
    public List<Transform> ListModel;
    public List<Material> ListOutFit;
    [SerializeField] protected List<Transform> PlayerPerform; 
    [SerializeField] protected List<SkinnedMeshRenderer> PlayerRen;
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
    protected void LoadPlayerMesh()
    {
        if(PlayerRen.Count > 0 ) return;
        foreach (Transform element in PlayerPerform)
        {
            SkinnedMeshRenderer Mesh = element.GetComponent<SkinnedMeshRenderer>();
            if(Mesh == null) Debug.LogWarning(element + "dont have Material");
            PlayerRen.Add(Mesh);
        }
    }
    protected override void LoadComponents()
    {
        this.LoadPlayerMesh();
        this.LoadListOutFit();
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
    protected virtual void LoadListOutFit()
    {
        if(ListOutFit.Count > 0) return;
        string path = "Player/Player_Outfit/";
        Material[] array = Resources.LoadAll<Material>(path);
        for(int i = 0 ; i < array.Length ; i++)
        {
            ListOutFit.Add(array[i]);
        }
    } 
    protected void ActiveModelbyname(string name)
    {
        // foreach(Transform model in ListModel)
        // {
        //     if(model.name == name)
        //     {
        //         model.gameObject.SetActive(true);
        //     }
        //     else   model.gameObject.SetActive(false);
        // }
        foreach(Material Model in ListOutFit)
        {
            if(Model.name == name)
            {
                //
            }
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
