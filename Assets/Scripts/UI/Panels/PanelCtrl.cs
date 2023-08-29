using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCtrl : MyBehaviour
{
    protected static PanelCtrl instance;
    [SerializeField] protected Transform HpbarHoler;
    public static PanelCtrl Instance { get => instance ;}
    public List<Transform> ListPanels;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadListPanel();
    } 
    protected void LoadListPanel()
    {
        if(ListPanels.Count > 0) return;
        foreach(Transform panel in this.transform)
        {
            this.ListPanels.Add(panel);
        }
    }
    protected void FixedUpdate()
    {
        if(DataManager.Instance.GamePlayMode) HpbarHoler.gameObject.SetActive(!this.transform.GetChild(0).gameObject.activeInHierarchy);
    }
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }
    public virtual void ShowPanel(string Panelname)
    {
        foreach(Transform panel in ListPanels)
        {
          if(panel.name == Panelname)   panel.gameObject.SetActive(true);
        }
    }
    public virtual void HirePanel(string Panelname)
    {
        foreach(Transform panel in ListPanels)
        {
              if(panel.name == Panelname)  panel.gameObject.SetActive(false);
        }
    }
}
