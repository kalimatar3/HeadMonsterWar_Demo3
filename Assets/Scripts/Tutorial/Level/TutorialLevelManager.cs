using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelManager : MyBehaviour
{
    public List<Transform> ListLevels;
    protected static TutorialLevelManager instance;
    public static TutorialLevelManager Instance { get => instance;}
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
        ListLevels[DataManager.Instance.TutorialLevel].gameObject.SetActive(true);
    }
    public void NextLevel()
    {
        DataManager.Instance.TutorialLevel = DataManager.Instance.TutorialLevel + 1;
        Lsmanager.Instance.SaveGame();
    }

}
