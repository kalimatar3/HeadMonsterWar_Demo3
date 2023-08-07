using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GunUIManager : MyBehaviour
{
    [SerializeField] protected List<RectTransform> ListGuns;
    [SerializeField] protected int Index;
    [SerializeField] protected RectTransform Holder;
    [SerializeField] protected RectTransform Status;
    [SerializeField] protected float ChangepageTime;
    [SerializeField] protected List<Slider> ListStatusSlider; 
    protected Vector3 BaseHolderPos;
    protected struct status
    {
        float Dame;
        float Range;
        float Firerate;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadListGuns();
        this.LoadStatusSlider();
    }
    protected void LoadStatusSlider()
    {
        if(ListStatusSlider.Count > 0) return;
        foreach(Transform element in Status )
        {
            if(element.GetComponentInChildren<Slider>())
            {
                ListStatusSlider.Add(element.GetComponentInChildren<Slider>());
            }
        }
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
        BaseHolderPos = Holder.localPosition;
        this.Index = 0; 
        this.SelectGun();
    }
    public void IncreaseIndex()
    {
        if(Index < ListGuns.Count-1)
        {
            this.Index = (this.Index + 1);
            this.HodlerMoveLeft();
            this.SelectGun();
        }
    }
    public void DecreaseIndex()
    {
        if(Index > 0) 
        {
            this.Index = (this.Index - 1);
            this.HolderMoveright();
            this.SelectGun();
        }
    }
    protected void HodlerMoveLeft()
    {
        Holder.transform.DOLocalMoveX(BaseHolderPos.x -1830 ,ChangepageTime).SetEase(Ease.Linear);
        BaseHolderPos.x -= 1830;       
    }
    protected void HolderMoveright()
    {
        Holder.transform.DOLocalMoveX(BaseHolderPos.x +1830 ,ChangepageTime).SetEase(Ease.Linear); 
        BaseHolderPos.x += 1830;    
    }
    protected void SelectGun()
    {
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
