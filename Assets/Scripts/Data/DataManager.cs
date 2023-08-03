using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DataManager : MyBehaviour
{
    [Serializable]
    public class Data
    {
        public int Gold;
        public string CurrentMap;
        public string CurrentModelName ;
        public int CurrentLevel;
        public  float MusicVolume,SoundEffectVolume;
        public List<ShopData> ListShopData;
        public List<UpgradeableData> ListUpGradeAbleData;
        public List<DropItemData> ListDropItemData;
    }
    [Serializable]
    public class ShopData
    {
        public string Name;
        public int Cost;
        public bool Available;

    }
    [Serializable]
    public class DropItemData
    {
        public string Name;
        public int CurrentUpgrade;
    }
    [Serializable] 
    public class UpgradeableData
    {
        public String Name;
        public int CurrentUpgrade;
    }
    public enum UpgradeabledataName
    {
        IcreMaxHPCost,
        IcreMaxbulletPistolCost,
        IcreCoinCost,
    }
    protected static DataManager instance;
    public static DataManager Instance { get => instance;}
    [Header("SAVED DATA")]
    public int Gold;
    public string CurrentMap;
    public string CurrentModelName;
    public  string CurrentGunName;
    public int CurrentLevel;
    public  float MusicVolume,SoundEffectVolume;
    public List<UpgradeableData> ListUpGradeAbleData;
    public List<DropItemData> ListDropItemData;
    public List<ShopData> ListShopData; 
    [Header("SO DATA")]
    [SerializeField] protected ShopSO ShopSO; 
    public List<EnemiesSO> ListEnemieSO;
    public List<BulletsSO> ListBulletsSO;
    public List<CostSO> ListCostSO;
    public List<DropItemSO> ListDropItemSO;
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
    protected override void Start()
    {
        base.Start();
        StartCoroutine(LoadListShopDatadelay());
        StartCoroutine(LoadListUpGradeAbledelay());
        StartCoroutine(LoadListDropItemDatadelay());
         StartCoroutine(UnlockDelay());
   }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShopSO();
        this.LoadEnemieSO();
        this.LoadBulletSO();
        this.LoadListCostSO();
        this.LoadListDropSO();
    }
    protected void LoadListCostSO()
    {
        if(ListCostSO.Count > 0 ) return;
        string rePath = "CostSO/";
        CostSO[]  array = Resources.LoadAll<CostSO>(rePath);
        for(int i = 0 ; i < array.Length; i ++)
        {
            ListCostSO.Add(array[i]);
        }
    }
    protected void LoadListDropSO()
    {
        if(ListDropItemSO.Count > 0 ) return;
        string rePath = "DropItemSO/";
        DropItemSO[]  array = Resources.LoadAll<DropItemSO>(rePath);
        for(int i = 0 ; i < array.Length; i ++)
        {
            ListDropItemSO.Add(array[i]);
        }
    }
    protected void LoadListDropItemData()
    {
        if(ListDropItemData.Count > 0) return;
        for(int i = 0 ; i < ListCostSO.Count; i ++)
        {
            ListDropItemData.Add(new DropItemData() {Name = ListDropItemSO[i].Name});
        }
    }
    protected IEnumerator LoadListDropItemDatadelay()
    {
        yield return new WaitUntil( predicate :() =>
        {
            if(ListDropItemSO.Count == 0) return false;
            return true;
        });
        this.LoadListDropItemData();
    }
    protected void LoadListUpGradeAble()
    {
        if(ListUpGradeAbleData.Count > 0) return;
        for(int i = 0 ; i < ListCostSO.Count; i ++)
        {
            ListUpGradeAbleData.Add(new UpgradeableData() {Name = ListCostSO[i].Name});
        }
    }
    protected IEnumerator LoadListUpGradeAbledelay()
    {
        yield return new WaitUntil( predicate :() =>
        {
            if(ListCostSO.Count == 0) return false;
            return true;
        });
        this.LoadListUpGradeAble();
    }

    protected void LoadEnemieSO()
    {
        if(ListEnemieSO.Count > 0 ) return;
            string rePath = "Enemies/";
            EnemiesSO[]  array = Resources.LoadAll<EnemiesSO>(rePath);
            for(int i = 0 ; i < array.Length; i ++ )
            {
                ListEnemieSO.Add(array[i]);
            }
    }
    protected void LoadBulletSO()
    {
        if(ListBulletsSO.Count > 0 ) return;
            string rePath = "Bullets/";
            BulletsSO[]  array = Resources.LoadAll<BulletsSO>(rePath);
            for(int i = 0 ; i < array.Length; i ++ )
            {
                ListBulletsSO.Add(array[i]);
            }
    }
    protected virtual void LoadShopSO()
    {
        string path = "ShopSO/ShopElement";
        if(Resources.Load<ShopSO>(path) == null) Debug.LogWarning(this.transform + "Dont have Shop");
        ShopSO = Resources.Load<ShopSO>(path);
    }
    protected virtual void LoadListShopData()
    {
        if(ListShopData.Count > 0) return;
        for(int i = 0 ; i < ShopSO.ListShopdata.Count ; i++)
        {
            ListShopData.Add(new ShopData(){Name = ShopSO.ListShopdata[i].Name,Available = ShopSO.ListShopdata[i].Available,Cost = ShopSO.ListShopdata[i].Cost });
        }
    }
    protected IEnumerator LoadListShopDatadelay()
    {
        yield return new WaitUntil( predicate :() =>
        {
            if(ShopSO.ListShopdata.Count == 0) return false;
            return true;
        });
        this.LoadListShopData();
    }
    public virtual int GetGold()
    {
        return this.Gold;
    }
    public string GetReferanceName(Transform obj)
    {
        return obj.transform.name;
    }
    public virtual void IcrGold(int number)
    {
        this.Gold += number ;
       Lsmanager.Instance.SaveGame();
    }
    public virtual void DcrGold(int number)
    {
        this.Gold -= number;
        Lsmanager.Instance.SaveGame();
    }
    protected virtual bool CanPayGold(int number)
    {
        if(number > Gold) return false;
        return true;
    }
    public virtual void Unlock(Transform obj)
    {
        foreach(ShopData element in ListShopData)
        {
            if(element.Name == GetReferanceName(obj))
            {
                if(!CanPayGold(element.Cost) || element.Cost == 0)
                {
                    element.Available = false; 
                    return;
                }                   
                this.DcrGold(element.Cost);
                ButtonManager.Instance.BuyButton.gameObject.SetActive(false);
                element.Available = true ;
            }
        }
    }
    protected int GetmaxUpgradefromUGAD(string Name)
    {
        foreach(CostSO element in ListCostSO)
        {
            if(Name == element.Name)
            {
                return element.Cost.Count;
            }
        }
        return 1000000;
    }
        protected int GetmaxUpgradefromDID(string Name)
    {
        foreach(DropItemSO element in ListDropItemSO)
        {
            if(Name == element.Name)
            {
                return element.Quality.Count;
            }
        }
        return 9999;
    }

    public int GetCost(string Name)
    {
        foreach(CostSO element in ListCostSO)
        {
            if(Name == element.Name)
            {
                if(GetUpgradenumberfromUGAD(Name) >= GetmaxUpgradefromUGAD(Name) -1) return 0;
                return element.Cost[GetUpgradenumberfromUGAD(Name)];
            }
        }
        return 10000;
    }
    protected virtual void UpgradefromUGAD(string name)
    {
        foreach(UpgradeableData element in ListUpGradeAbleData)
        {
            if(name == element.Name)  
            {
                if(element.CurrentUpgrade >= GetmaxUpgradefromUGAD(element.Name) - 1) return;
                element.CurrentUpgrade = (element.CurrentUpgrade + 1);
                SoundSpawner.Instance.Spawn(CONSTSoundsName.Upgrade,Vector3.zero,Quaternion.identity);
            }
        }
    }
    public int GetUpgradenumberfromUGAD(string name)
    {        
        foreach(UpgradeableData element in ListUpGradeAbleData)
        {
            if(name == element.Name) return element.CurrentUpgrade; 
        }
        return 0 ;
    }
    protected virtual void UpgradefromDID(string name)
    {
        foreach(DropItemData element in ListDropItemData)
        {
            if(element.CurrentUpgrade >= GetmaxUpgradefromDID(element.Name) -1) return;
            if(name == element.Name)  element.CurrentUpgrade = (element.CurrentUpgrade + 1); 
        }
    }
    public int GetUpgradenumberfromDID(string name)
    {        
        foreach(DropItemData element in ListDropItemData)
        {
            if(name == element.Name) return element.CurrentUpgrade; 
        }
        return 0 ;
    }

    public virtual void IcrCoinEarn()
    {
        if(!CanPayGold(GetCost(UpgradeabledataName.IcreCoinCost.ToString()))) return;
        this.DcrGold(GetCost(UpgradeabledataName.IcreCoinCost.ToString()));
        this.UpgradefromUGAD(UpgradeabledataName.IcreCoinCost.ToString());
        this.UpgradefromDID("Coin");
        Lsmanager.Instance.SaveGame();
    }

    public virtual void IcrMaxHp()
    {
        if(!CanPayGold(GetCost(UpgradeabledataName.IcreMaxHPCost.ToString()))) return;
        this.DcrGold(GetCost(UpgradeabledataName.IcreMaxHPCost.ToString()));
        this.UpgradefromUGAD(UpgradeabledataName.IcreMaxHPCost.ToString());
        Lsmanager.Instance.SaveGame();
    }
    public virtual void IcrMaxbullet()
    {
        if(!CanPayGold(GetCost(UpgradeabledataName.IcreMaxbulletPistolCost.ToString()))) return;
        this.DcrGold(GetCost(UpgradeabledataName.IcreMaxbulletPistolCost.ToString()));
        this.UpgradefromUGAD(UpgradeabledataName.IcreMaxbulletPistolCost.ToString());
        Lsmanager.Instance.SaveGame();
    }
    public virtual void PlayerDataFromJson(string JsonString)
    {
       Data obj = JsonUtility.FromJson<Data>(JsonString);
       if(obj == null) return;
        this.Gold = obj.Gold;
        this.CurrentMap = obj.CurrentMap;
        this.CurrentModelName = obj.CurrentModelName;
        this.ListUpGradeAbleData = obj.ListUpGradeAbleData;
        this.MusicVolume = obj.MusicVolume;
        this.SoundEffectVolume = obj.SoundEffectVolume;
        this.ListDropItemData = obj.ListDropItemData;
        this.ListShopData = obj.ListShopData;
        this.CurrentLevel = obj.CurrentLevel;
    }
    public virtual void ClearJson()
    {
        this.CurrentMap = "Map1";
        this.CurrentModelName = "Model0";
        this.Gold = 99999;
        this.ListUpGradeAbleData = null;
        this.ListDropItemData = null;
        this.ListShopData = null;
        this.CurrentLevel = 0;
   }
   protected IEnumerator UnlockDelay()
   {
        yield return new WaitUntil(predicate : () => 
        { 
        if(ListShopData.Count == 0) return false;
        return true;
        });
        foreach(Transform unlocked in ButtonManager.Instance.Listlockbutton)
        {
            foreach(ShopData element in ListShopData)
            {
                if(unlocked.name == element.Name)
                {
                    unlocked.gameObject.SetActive(!element.Available);
                }
            }
        }
   }
}
