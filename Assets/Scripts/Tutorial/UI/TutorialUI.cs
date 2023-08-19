using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TutorialUI : MyBehaviour
{
    protected static TutorialUI instance;
    public static TutorialUI Instance { get => instance ;}
    [SerializeField] Camera ThisCam;
    [SerializeField] protected List<Transform> ListPanels;
    [SerializeField] protected List<Transform> Allbutton;
    [SerializeField] protected List<Trans> ListActivebutton;
    [SerializeField] protected Transform TutorialPoint;
    [Serializable]
    public struct Trans
    {
        public List<Transform> List;
    } 
    protected override void Awake()
    {
        base.Awake();
        if(instance != this && instance != null) Destroy(this);
        else instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadListPanel();
    }
    public void SetCollorTutorialPoint(Transform TarGet,Color color)
    {
        TutorialPointFollow Thisfollow = TutorialPoint.GetComponentInChildren<TutorialPointFollow>();
        Thisfollow.Obj = TarGet;
        SpriteRenderer TutorialPointImage = Thisfollow.transform.GetComponentInChildren<SpriteRenderer>();
        TutorialPointImage.color = color;
    }
    public void SetMassage(String Mes,Color color)
    {
        TextMeshPro TutorialPointText = TutorialPoint.GetComponentInChildren<TextMeshPro>();
        TutorialPointText.text = Mes;
        TutorialPointText.color = color;
    }
    public void LoadListPanel()
    {
        if(ListPanels.Count > 0) return;
        foreach(Transform element in this.transform)
        {
            ListPanels.Add(element);
        }
    }
    public Vector3 ChangetoWorldPoint(Vector3 Pos)
    {
        return ThisCam.ScreenToWorldPoint(Pos);
    }
    public void ActivePanel(int index)
    {
        for(int i = 0 ; i < ListPanels.Count; i++)
        {
            ListPanels[i].gameObject.SetActive(false);
        }
        ListPanels[index].gameObject.SetActive(true);
    }
    public void ActiveTutorialPoint()
    {
        this.TutorialPoint.gameObject.SetActive(true);
    }
    public void DeActiveTutorialPoint()
    {
        this.TutorialPoint.gameObject.SetActive(false);
    }
    public void DeActivePanel()
    {
        for(int i = 0 ; i < ListPanels.Count; i++)
        {
            ListPanels[i].gameObject.SetActive(false);
        }
    }
    public void ActiveButton(int index)
    {
        foreach(Transform element in Allbutton)
        {
            element.gameObject.SetActive(false);
        }
        foreach(Transform ele in ListActivebutton[index].List)
        {
            ele.gameObject.SetActive(true);
        }
    }
    public void ActiveAll()
    {
        foreach(Transform element in Allbutton)
        {
            element.gameObject.SetActive(true);
        }
    }
}

