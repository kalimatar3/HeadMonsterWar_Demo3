using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MyBehaviour
{    
    protected static BuffManager instance;
    public static BuffManager Instance { get => instance ;}
    public Transform CurrentBuff,CurrentBuffEffect;
    public List<float> ListLimitNumberofBuff;
    public List<float> ListCurrentNumberofBuff;
    protected override void Awake()
    {
        base.Awake();
        if(instance != null && instance != this)
        {
            Destroy(this);
            Debug.LogWarning(this.gameObject + "Existed");
        }
        else instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCurrentBuff();
    }
    protected void LoadCurrentBuff()
    {
        this.CurrentBuff = this.transform.parent;
        if(ListLimitNumberofBuff.Count <= 0 ) Debug.LogWarning(this.transform.parent + "Don't Have ListLimit");
        foreach(float element in ListLimitNumberofBuff)
        {
            this.ListCurrentNumberofBuff.Add(0);
        }
    }
    public void DeductCurrentNumberofBuff(string Name)
    {
        for(int i = 0 ; i < BuffSpawner.Instance.Buffname.Count ; i++)
        {
            if(Name == BuffSpawner.Instance.Buffname[i])
            {
                BuffManager.Instance.ListCurrentNumberofBuff[i] --; 
            }
        }
    }
    public void IncreaseCurrentNumberofBuff(string Name)
    {
        for(int i = 0 ; i < BuffSpawner.Instance.Buffname.Count ; i++)
        {
            if(Name == BuffSpawner.Instance.Buffname[i])
            {
                BuffManager.Instance.ListCurrentNumberofBuff[i] ++; 
            }
        }       
    }
}
