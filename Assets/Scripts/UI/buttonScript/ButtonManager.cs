using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class ButtonManager : MyBehaviour
{
    protected static ButtonManager instance;
    public static ButtonManager Instance { get => instance;}
    public List<RectTransform> Listlockbutton;
    public List<RectTransform> ListSelectButton;
    public RectTransform Currentbutton;
    public RectTransform SelectButton;
    public RectTransform  ApprearButton;
    public RectTransform BuyButton;
    public RectTransform cache;
    public RectTransform DailyRewardButton;
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
        this.LoadListLockbutton();
    }
    protected void LoadListLockbutton()
    {
        if(Listlockbutton.Count  > 0) return;
        foreach(RectTransform  element in ListSelectButton)
        {
            LockButton  elementbutton = element.GetComponentInChildren<LockButton>();
            RectTransform   elementRT = elementbutton.GetComponent<RectTransform>();
            Listlockbutton.Add(elementRT);
        }
    }
    public void Showpanel(string panelname)
    {
        this.Buttonsound();
        PanelCtrl.Instance.ShowPanel(panelname);
    }
    public void HirePanel(string Panelname)
    {
        this.Buttonsound();
        PanelCtrl.Instance.HirePanel(Panelname);
    }
    public void ClearData()
    {
        Lsmanager.Instance.ClearData();
    }
    public void Reborn()
    {
        GunCtrl.Instance.Shooting.reborn();
        PlayerController.Instance.PlayerReciver.ReBorn();
        HolderManager.Instance.ActiveHolder("SoundHolder");
    }
    public void Revive()
    {
        this.Reborn();
        EffectSpawner.Instance.Spawn(CONSTEffect.ReviveEffect,PlayerController.Instance.transform.position,Quaternion.identity);
        SoundSpawner.Instance.Spawn(CONSTSoundsName.Revive,Vector3.zero,Quaternion.identity);
        this.StartCoroutine(CanreviveDelay());
    }
    protected IEnumerator CanreviveDelay()
    {
        yield return new WaitUntil(predicate:()=>
        {
            if(PlayerController.Instance.PlayerReciver.CurrentHp == 0) return false;
            return true;
        });
        PlayerController.Instance.PlayerReciver.canRevise();
    }
    public void Replay()
    {
        GameManager.Instance.Replay();
    }
    public void Reload()
    {
        GunCtrl.Instance.Shooting.Reloading();
    }
    public void LoadMap(Transform obj)
    {
        DataManager.Instance.CurrentMap = DataManager.Instance.GetReferanceName(obj);
        ScenesManager.Instance.LoadScene(SceneManager.GetActiveScene().name);
        Lsmanager.Instance.SaveGame();
    }
    public void LoadPlayerModel(Transform obj)
    {
        if(ModelUIManager.Instance != null)
        ModelUIManager.Instance.ActiveModel(obj.name);
    }
    public void OneLick(Transform thisTrans)
    {
       thisTrans.gameObject.SetActive(false);
    }
    public void InvulnerablePlayer()
    {
        this.Buttonsound();
        EffectSpawner.Instance.Spawn(CONSTEffect.InvulnerableEffect,PlayerController.Instance.transform.position,Quaternion.identity);
        SoundSpawner.Instance.Spawn(CONSTSoundsName.Invulnerable,Vector3.zero,Quaternion.identity);
        PlayerController.Instance.PlayerReciver.invulnerable();
    }
    protected void Buttonsound()
    {
        SoundSpawner.Instance.Spawn(CONSTSoundsName.ButtonTap,Vector3.zero,Quaternion.identity);
    }
    public void AppearButtonState()
    {
        this.Appearbutton();
        if(Currentbutton == null) return;
        this.BuyState();
        this.SelectState();
        this.SlectedState();
    }
    protected void Appearbutton()
    {
        for(int i = 0 ; i < ShopPanelManager.Instance.ListPanels.Count ; i++)
        {
            if(ShopPanelManager.Instance.ListPanels[i].gameObject.activeInHierarchy)
            {
                this.ApprearButton.gameObject.SetActive(true);
                return;
            }
        }
        this.ApprearButton.gameObject.SetActive(false);
    }
    public void ClickDailyReward()
    {
        DataManager.Instance.LastTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        DataManager.Instance.CanshowDailyReward = false;
        CoinUISpawner.Instance.CurrentNumberofCoins = 100;
        Lsmanager.Instance.SaveGame();
    }
    protected void FixedUpdate()
    {
        this.AppearButtonState();
        if(Currentbutton == null) Currentbutton = cache;
    }
    public void SettingMusicVolume()
    {
        SoundManager.Instance.SettingMusicVolume();
        Lsmanager.Instance.SaveGame();
    }
    public void SettingSoundEffectVolume()
    {
        SoundManager.Instance.SettingSoundEffectVolume();
        Lsmanager.Instance.SaveGame();
    }    
    public void ClickSelect()
    {
        this.Buttonsound();
        Lsmanager.Instance.SaveGame();
    }
    public void ClickBuy()
    {
        this.Buttonsound();
        DataManager.Instance.Unlock(Currentbutton);
        Lsmanager.Instance.SaveGame();
    }
    protected void BuyState()
    {
        foreach(Transform locked in Listlockbutton)
        {
            if(locked.name == Currentbutton.name)
            {
                BuyButton.gameObject.SetActive(locked.gameObject.activeInHierarchy);
                return;
            }
        }
    }
    protected void SelectState()
    {
        foreach(Transform locked in ListSelectButton)
        {
            if(locked.name == Currentbutton.name)
            {
                SelectButton.gameObject.SetActive(locked.gameObject.activeInHierarchy);
                return;
            }
        }
    }
    protected void SlectedState()
    {        
        if(DataManager.Instance  ==  null)  return;
        if(Currentbutton.name == DataManager.Instance.CurrentModelName ||  Currentbutton.name == DataManager.Instance.CurrentGunName) 
        {
            BuyButton.gameObject.SetActive(false);
            SelectButton.gameObject.SetActive(false);
        }
    }
    public void ClickPauseGame()
    {
        GameManager.Instance.PauseGame();
    }
    public void ClickResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }
}
