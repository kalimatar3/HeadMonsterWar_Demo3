using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

public class ButtonManager : MyBehaviour
{
    protected static ButtonManager instance;
    public static ButtonManager Instance { get => instance;}
    public List<RectTransform> Listlockbutton;
    public List<RectTransform> ListSelectButton;
    public RectTransform Currentbutton;
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
        ManagerAds.ins.ShowRewarded((x)=>
        {
        this.Reborn();
        EffectSpawner.Instance.Spawn(CONSTEffect.ReviveEffect,PlayerController.Instance.transform.position,Quaternion.identity);
        SoundSpawner.Instance.Spawn(CONSTSoundsName.Revive,Vector3.zero,Quaternion.identity);
        this.StartCoroutine(CanreviveDelay());
       });
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
        StartCoroutine(ScenesManager.Instance.LoadScene(SceneManager.GetActiveScene().name));
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
       ManagerAds.ins.ShowRewarded((x)=>
        {
         this.Buttonsound();
        EffectSpawner.Instance.Spawn(CONSTEffect.InvulnerableEffect,PlayerController.Instance.transform.position,Quaternion.identity);
        SoundSpawner.Instance.Spawn(CONSTSoundsName.Invulnerable,Vector3.zero,Quaternion.identity);
        PlayerController.Instance.PlayerReciver.invulnerable();
       });
    }
    protected void Buttonsound()
    {
        SoundSpawner.Instance.Spawn(CONSTSoundsName.ButtonTap,Vector3.zero,Quaternion.identity);
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
        this.StartCoroutine(Load());
        ModelManager.Instance.ActiveModel();
        GunCtrl.Instance.ActiveGun();
        Lsmanager.Instance.SaveGame();
    }
    protected IEnumerator Load()
    {
        yield return new WaitUntil(predicate:()=>
        {
            if(DataManager.Instance == null) return false;
            return true;
        });
        yield return new WaitUntil(predicate:()=>
        {
            foreach(RectTransform ele in Listlockbutton)
            {
                if(Currentbutton == ele || Currentbutton == null) return false;
                if(Currentbutton != ele) return true;
            }
            return true;
        });
        foreach(Transform element in ModelManager.Instance.ListModel)
        {
            if(element.name == Currentbutton.name)
            {
                DataManager.Instance.CurrentModelName = element.name;
            }
        }
        foreach(Transform element in GunCtrl.Instance.ListGuns)
        {
            if(element.name == Currentbutton.name)
            {
                DataManager.Instance.CurrentGunName = element.name;
            }
        }
        Lsmanager.Instance.SaveGame();
    }
    public void ClickPauseGame()
    {
        GameManager.Instance.PauseGame();
    }
    public void ClickResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }
    public void IncreaseCoin(int value)
    {
        ManagerAds.ins.ShowRewarded((x)=>{
        DataManager.Instance.IcrGold(value);
        });
    }
}
