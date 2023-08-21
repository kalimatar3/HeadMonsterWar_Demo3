using System.Collections;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
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
    protected IEnumerator TimeCounting()
    {
        yield return new WaitUntil(predicate:() =>
        {
            if(!DataManager.Instance.GamePlayMode) return false;
            if(!PanelCtrl.Instance.transform.GetChild(0).gameObject.activeInHierarchy) return false;
            else
            {
                ButtonManager.Instance.DailyRewardButton.gameObject.SetActive(DataManager.Instance.CanshowDailyReward);
                timepassed = DateTime.Now - DateTime.Parse(DataManager.Instance.LastTime);
                if(timepassed.TotalHours < 24f) return false;
                else return true;
            }
        });
        DataManager.Instance.LastTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        PanelCtrl.Instance.ShowPanel("DailyRewardpannel");
    }
    protected IEnumerator GameModeDelay()
    {
        yield return new WaitUntil(predicate:()=>
        {
            if(DataManager.Instance == null) return false;
            return true;
        });
        this.GameMode();
    }
    protected void GameMode()
    {
        if(DataManager.Instance.TutorialLevel > 2) DataManager.Instance.GamePlayMode = true;
        if(DataManager.Instance.GamePlayMode)
        {
            TutorialUI.Instance.ActiveAll();
            PanelCtrl.Instance.HirePanel("TutorialPanel");
            PanelCtrl.Instance.ShowPanel("MainMenuPannel");
        }
    }
    public void Replay()
    {
      ScenesManager.Instance.LoadScene(SceneManager.GetActiveScene().name);
      this.StartCoroutine(MapManager.Instance.DelayLoadMap());
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
        this.StartCoroutine(this.TimeCounting());
        this.StartCoroutine(this.GameModeDelay());
    }

}
