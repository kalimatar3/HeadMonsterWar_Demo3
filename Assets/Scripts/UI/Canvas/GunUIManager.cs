using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunUIManager : MyBehaviour
{
    [SerializeField] protected List<RectTransform> ListGuns;
    [SerializeField] protected int Index;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadListGuns();
    }
    protected void LoadListGuns()
    {
        if(ListGuns.Count > 0 ) return;
        foreach(RectTransform element in this.transform)
        {
            ListGuns.Add(element);
        }
    }
    protected override void Start()
    {
        base.Start();
        this.Index = 0; 
        this.ActivePannel();
    }
    public void IncreaseIndex()
    {
        this.Index = (this.Index + 1) %   ListGuns.Count;
        this.ActivePannel();
    }
    public void DecreaseIndex()
    {
        if(Index > 0)
        this.Index = (this.Index - 1);
        else
        {
            this.Index = ListGuns.Count + (this.Index - 1);
        }
        this.ActivePannel();
    }
    protected void ActivePannel()
    {
        for(int i = 0 ; i < ListGuns.Count ; i++)
        {
            ListGuns[i].gameObject.SetActive(false);        
        }
       ListGuns[Index].gameObject.SetActive(true);
       if(ButtonManager.Instance != null) 
       {
            foreach(RectTransform element in ListGuns[Index])
            {
               Button elementbutton  = element.GetComponent<Button>();
               if(elementbutton !=null &&  element.gameObject.activeInHierarchy)
                {
                    ButtonManager.Instance.Currentbutton  = element;
                    return;
                }
            }
            ButtonManager.Instance.Currentbutton = ListGuns[Index];
       }
    }
}
