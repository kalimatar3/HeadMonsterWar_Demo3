using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class PanelCtrl : MyBehaviour
{
    protected static PanelCtrl instance;
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
