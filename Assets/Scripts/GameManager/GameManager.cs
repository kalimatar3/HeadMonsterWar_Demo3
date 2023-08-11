using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MyBehaviour
{
    protected static GameManager instance;
    public static GameManager Instance { get => instance ;}
    protected TimeSpan timepassed;
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
    protected IEnumerator FakeUpdate()
    {
        foreach(Transform panel in PanelCtrl.Instance.ListPanels)
        {
          if(panel.name == "MainMenuPannel") 
          {
            while(panel.gameObject.activeInHierarchy)
            {
                yield return new WaitForSeconds(2f);
                timepassed = DateTime.Now - DateTime.Parse(DataManager.Instance.LastTime);
                Debug.Log(timepassed.TotalHours);
                if(timepassed.TotalHours >= 24f)
                {
                    ButtonManager.Instance.DailyRewardButton.gameObject.SetActive(true);
                    DataManager.Instance.LastTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    Lsmanager.Instance.SaveGame();
                }
            }
          }
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("Pause");
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Resume");
    }
    protected override void Start()
    {
        base.Start();
        this.StartCoroutine(this.FakeUpdate());
    }

}
