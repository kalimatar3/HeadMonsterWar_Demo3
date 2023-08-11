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
        public string CurrentGunName;
        public int CurrentLevel;
        public  float MusicVolume,SoundEffectVolume;
        public List<ShopData> ListShopData;
        public List<UpgradeableData> ListUpGradeAbleData;
        public List<DropItemData> ListDropItemData;
        public string LastTime;
        public float Lasthours;
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
        public List<element> ListUpdate;
        [Serializable]
        public class element
        {
            public String Name;
            public int CurrentUpgrade;
        }
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
    public String LastTime;
    [Header("SO DATA")]
    [SerializeField] protected ShopSO ShopSO; 
    public List<EnemiesSO> ListEnemieSO;
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
        this.LastTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Debug.Log(LastTime);
        StartCoroutine(LoadListUpGradeAbledelay());
        StartCoroutine(LoadListDropItemDatadelay());
        StartCoroutine(LoadListShopDatadelay());
        StartCoroutine(UnlockDelay());
   }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShopSO();
        this.LoadEnemieSO();
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
        for(int i = 0 ; i < ListCostSO.Count ; i++)
        {
            ListUpGradeAbleData.Add(new UpgradeableData() { ListUpdate = new List<UpgradeableData.element>()});
            for(int j = 0 ; j < ListCostSO[i].List.Count ; j++)
            {
                UpgradeableData.element thiselement = new UpgradeableData.element() {Name = ListCostSO[i].List[j].Name};
                ListUpGradeAbleData[i].ListUpdate.Add(thiselement);
            }
        }
    }
    protected IEnumerator LoadListUpGradeAbledelay()
    {
        yield return new WaitUntil( predicate :() =>
        {
            if(ListCostSO.Count <= 0) return false;
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
        ButtonManager.Instance.cache = obj.parent.GetComponent<RectTransform>();
        foreach(ShopData element in ListShopData)
        {
            if(element.Name == GetReferanceName(obj))
            {
                if(!CanPayGold(element.Cost) || element.Cost  == 0)
                {
                    element.Available = false; 
                    return;
                }                   
                this.DcrGold(element.Cost);
                ButtonManager.Instance.BuyButton.gameObject.SetActive(false);
                element.Available = true ;
                obj.gameObject.SetActive(false);
            }
        }
    }
    protected int GetmaxUpgradefromUGAD(string Name)
    {
        foreach(CostSO element in ListCostSO)
        {
            for(int i = 0; i < element.List.Count ; i++)
            {
                if(Name == element.List[i].Name)
                {
                    return element.List[i].Cost.Count;
                }
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
            for(int i = 0 ; i < element.List.Count ; i ++)
            {
                if(Name == element.List[i].Name)
                {
                     if(GetUpgradenumberfromUGAD(Name) >= GetmaxUpgradefromUGAD(Name) -1) return 0;
                     return element.List[0].Cost[GetUpgradenumberfromUGAD(Name)];
                }
            }
        }
        return 10000;
    }
    protected virtual void UpgradefromUGAD(string name)
    {
        foreach(UpgradeableData element in ListUpGradeAbleData)
        {
            for(int i  = 0 ; i < element.ListUpdate.Count; i ++)
            {
                if(name == element.ListUpdate[i].Name)  
                {
                    if(element.ListUpdate[i].CurrentUpgrade >= GetmaxUpgradefromUGAD(element.ListUpdate[i].Name) - 1) return;
                    element.ListUpdate[i].CurrentUpgrade ++;
                    SoundSpawner.Instance.Spawn(CONSTSoundsName.Upgrade,Vector3.zero,Quaternion.identity);
                }
            }
        }
    }
    public int GetUpgradenumberfromUGAD(string name)
    {        
        foreach(UpgradeableData element in ListUpGradeAbleData)
        {
            for(int i = 0 ; i < element.ListUpdate.Count ;i ++)
            {
                if(name == element.ListUpdate[i].Name) return element.ListUpdate[i].CurrentUpgrade; 
            }
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
        if(!CanPayGold(GetCost("Coin"))) return;
        this.DcrGold(GetCost("Coin"));
        this.UpgradefromUGAD("Coin");
        this.UpgradefromDID("Coin");
        Lsmanager.Instance.SaveGame();
    }

    public virtual void IcrMaxHp()
    {
        if(!CanPayGold(GetCost("Hp"))) return;
        this.DcrGold(GetCost("Hp"));
        this.UpgradefromUGAD("Hp");
        Lsmanager.Instance.SaveGame();
    }
    public virtual void IcrMaxbullet()
    {
         if(!CanPayGold(GetCost(CurrentGunName))) return;
        this.DcrGold(GetCost(CurrentGunName));
        this.UpgradefromUGAD(CurrentGunName);
        Lsmanager.Instance.SaveGame();    
    }
    public virtual void PlayerDataFromJson(string JsonString)
    {
       Data obj = JsonUtility.FromJson<Data>(JsonString);
       if(obj == null) return;
        this.Gold = obj.Gold;
        this.CurrentMap = obj.CurrentMap;
        this.CurrentModelName = obj.CurrentModelName;
        this.CurrentGunName = obj.CurrentGunName;
        this.ListUpGradeAbleData = obj.ListUpGradeAbleData;
        this.MusicVolume = obj.MusicVolume;
        this.SoundEffectVolume = obj.SoundEffectVolume;
        this.ListDropItemData = obj.ListDropItemData;
        this.ListShopData = obj.ListShopData;
        this.CurrentLevel = obj.CurrentLevel;
        this.LastTime = obj.LastTime;
    }
    public virtual void ClearJson()
    {
        this.CurrentMap = "Map1";
        this.CurrentModelName = "Model0";
        this.CurrentGunName = "Ak47";
        this.LastTime = "2023-08-09 15:30:45";
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
