using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class test : MyBehaviour
{
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
    public List<CostSO> ListCostSO;
    public List<UpgradeableData> ListUpGradeAbleData;
    [SerializeField] protected UpgradeableData cache;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadListCostSO();
    } 
    protected override void Start()
    {      
        this.StartCoroutine(LoadListUpGradeAbledelay());
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

}
