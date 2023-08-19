using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class TutorialLevel2 : TutorialLevel
{   
    protected GameObject FakeObj;
    [SerializeField] Transform GamePlayPanel;
    [SerializeField] Transform FistEnemy;
    [SerializeField] Transform FistEnemyBar;
    [SerializeField] Transform FistBuff;
    [SerializeField] protected Transform FistBoss;
    protected void OnEnable()
    {
        this.StartCoroutine(this.Flow());
    }
    protected IEnumerator Flow()
    {
        yield return new  WaitUntil(predicate:()=>
        {
            if(TutorialUI.Instance == null) return false;
            return true;
        });
        PanelCtrl.Instance.ShowPanel("MainMenuPannel");
        PanelCtrl.Instance.HirePanel("GameplayPanel");
        TutorialUI.Instance.ActiveButton(2);
        yield return new WaitForSeconds(1f);
        FakeObj =  new GameObject();
        FakeObj.transform.position =  new Vector3(-5,0,-23.5f);
        TutorialUI.Instance.ActivePanel(6);
        GameManager.Instance.PauseGame();
        yield return new  WaitUntil(predicate:()=>
        {
            if(PlayerController.Instance.PlayerReciver.MaxHp <= 100) return false;
            return true; 
        });
        GameManager.Instance.ResumeGame();
        TutorialUI.Instance.DeActivePanel();
        TutorialUI.Instance.DeActiveTutorialPoint();
        yield return new WaitForSeconds(1f);
        TutorialUI.Instance.ActivePanel(7);
        TutorialUI.Instance.ActiveButton(3);
        GameManager.Instance.PauseGame();
        yield return new  WaitUntil(predicate:()=>
        {
            return GamePlayPanel.gameObject.activeInHierarchy;
        });
        GameManager.Instance.ResumeGame();
        TutorialUI.Instance.DeActivePanel();    
        yield return new  WaitUntil(predicate:()=>
        {
            for( int i = 0 ; i < HolderManager.Instance.ListHolder.Count ; i++)
            {
                if(HolderManager.Instance.ListHolder[i].name == "EnemiesHolder")
                {
                    if(HolderManager.Instance.ListHolder[i].childCount <= 2) return false;
                    return true;
                }
            }
            return false;
        });
        this.FistEnemy.gameObject.SetActive(true);
        this.FistEnemyBar.gameObject.SetActive(true);
        yield return new  WaitUntil(predicate:()=>
        {
            return !FistEnemy.gameObject.activeInHierarchy;
        });
        FistBuff.position = FistEnemy.transform.position;
        FistBuff.gameObject.SetActive(true);
        TutorialUI.Instance.ActiveTutorialPoint();
        TutorialUI.Instance.SetCollorTutorialPoint(FistBuff,Color.yellow);
        TutorialUI.Instance.SetMassage("Power Up Item",Color.white);
        yield return new  WaitUntil(predicate:()=>
        {
            return !FistBuff.gameObject.activeInHierarchy;
        });
        TutorialUI.Instance.DeActiveTutorialPoint();
        yield return new  WaitUntil(predicate:()=>
        {
            for( int i = 0 ; i < HolderManager.Instance.ListHolder.Count ; i++)
            {
                if(HolderManager.Instance.ListHolder[i].name == "BossesHolder")
                {
                    if(HolderManager.Instance.ListHolder[i].childCount >0) 
                    FistBoss = HolderManager.Instance.ListHolder[i].GetChild(0);
                }
            }
            if(FistBoss == null) return false;
            return FistBoss.gameObject.activeInHierarchy;
        });
        yield return new WaitForSeconds(4f);
        TutorialUI.Instance.ActivePanel(2);
        GameManager.Instance.PauseGame();
        yield return new  WaitUntil(predicate:()=>
        {
            if(PlayerController.Instance.PlayerReciver.CurinvulnerableNumber <= 0) return false;
            return true;
        });
        GameManager.Instance.ResumeGame();
        TutorialUI.Instance.DeActivePanel();
               yield return new  WaitUntil(predicate:()=>
        {
            if(FistBoss.gameObject.activeInHierarchy) return false;
            return true;
        });
        yield return new WaitForSeconds(3f);
        this.IsDone = true;
        PanelCtrl.Instance.ShowPanel("CoinPanel");
    }   

}
