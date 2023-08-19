using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MyBehaviour
{
    public List<Transform> ListLevels;
    public float NumberofAllCE,NumberofPreCE,NumberofAliveCE;
    public string CrLevelname;
    protected static LevelManager instance;
    public static LevelManager Instance { get => instance;}

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
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadListLevels();
    }
    protected void LoadListLevels()
    {
        if(ListLevels.Count > 0 ) return;
        foreach(Transform element in this.transform)
        {
            ListLevels.Add(element);
        }
    }
    protected override void Start()
    {
        base.Start();
        this.StartCoroutine(LoadLevelDelay());
    }
    protected IEnumerator LoadLevelDelay()
    {
        yield return new WaitUntil(predicate:()=>
        {
            if(DataManager.Instance == null) return false;
            return true;
        });
        for(int i = 0 ; i < ListLevels.Count; i ++)
        {
            ListLevels[i].gameObject.SetActive(false);
        }
        ListLevels[DataManager.Instance.CurrentLevel].gameObject.SetActive(true);
    }
    public void NextLevel()
    {
        DataManager.Instance.CurrentLevel = ( DataManager.Instance.CurrentLevel + 1 );
        if(DataManager.Instance.CurrentLevel == ListLevels.Count) DataManager.Instance.CurrentLevel = 1;
        Lsmanager.Instance.SaveGame();
    }
}
