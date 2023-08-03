using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanelManager : MyBehaviour
{
    protected static ShopPanelManager instance;
    public static ShopPanelManager Instance { get => instance;}
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

}
