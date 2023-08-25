using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GunUIManager : MyBehaviour
{
    [SerializeField] protected List<RectTransform> ListGuns;
    [SerializeField] protected List<float> ListGunxPos;
    [SerializeField] public int Index;
    [SerializeField] protected RectTransform Holder;
    [SerializeField] protected RectTransform Status;
    [SerializeField] protected float ChangepageTime;
    [SerializeField] protected List<Slider> ListStatusSlider; 
    [SerializeField] protected float BaseHolderPosX;
    protected float CurrentVelocity0 = 0f,CurrentVelocity1 = 0f,CurrentVelocity2 = 0f;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStatusSlider();
        this.LoadListGuns();
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
        float basePOs = Holder.localPosition.x;
        foreach(RectTransform element in Holder)
        {
            ListGuns.Add(element);
            ListGunxPos.Add(basePOs);
            basePOs -= 1800;
        }
    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(this.TakeStartIndexDelay());
    }
    protected void OnEnable()
    {
        StartCoroutine(this.TakeStartIndexDelay());
        this.HolderMoveTo(Index);
        this.SelectGun();
    }
    protected IEnumerator TakeStartIndexDelay()
    {   
        yield return new WaitUntil(predicate :()=>
        {
            if(DataManager.Instance.CurrentGunName == null) return false;
            return true;
        });
        this.TakeStartIndex();
        this.HolderMoveTo(Index);
        this.SelectGun();
    }
    protected void TakeStartIndex()
    {
        for(int i = 0 ; i < GunCtrl.Instance.ListGuns.Count ; i++)
        {
            if(ListGuns[i].name == DataManager.Instance.CurrentGunName)
            {
                Index = i;
                return;
            }
        }
        Index = 0;
    }
    public void IncreaseIndex()
    {
        if(Index < ListGuns.Count-1)
        {
            this.Index = (this.Index + 1);
            HolderMoveTo(Index);
            this.SelectGun();
        }
    }
    public void DecreaseIndex()
    {
        if(Index > 0) 
        {
            this.Index = (this.Index - 1);
            HolderMoveTo(Index);
            this.SelectGun();
        }
    }
    protected void HolderMoveTo(int index)
    {
        Holder.transform.DOLocalMoveX( ListGunxPos[index],ChangepageTime).SetEase(Ease.Linear);
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
       //this.SliderStatus();
    }
    protected void SliderStatus()
    {
        float number0 =  GunCtrl.Instance.ListGunSO[Index].GunUpgrade[DataManager.Instance.GetUpgradenumberfromUGAD(GunCtrl.Instance.ListGunSO[Index].ToString())].Dame/150;
        float Currentvalue0 = Mathf.SmoothDamp(ListStatusSlider[0].value,number0,ref CurrentVelocity0,Time.deltaTime * 1f);
        ListStatusSlider[0].value = Currentvalue0;

        float number1 =  GunCtrl.Instance.ListGunSO[Index].GunUpgrade[DataManager.Instance.GetUpgradenumberfromUGAD(GunCtrl.Instance.ListGunSO[Index].ToString())].Range/100;
        float Currentvalue1 = Mathf.SmoothDamp(ListStatusSlider[1].value,number1,ref CurrentVelocity1, Time.deltaTime * 1f);
        ListStatusSlider[1].value = Currentvalue1;

        float number2 = 0.1f/(GunCtrl.Instance.ListGunSO[Index].GunUpgrade[DataManager.Instance.GetUpgradenumberfromUGAD(GunCtrl.Instance.ListGunSO[Index].ToString())].Firerate);
        float Currentvalue2 = Mathf.SmoothDamp(ListStatusSlider[2].value,number2,ref CurrentVelocity2,Time.deltaTime * 1f);
        ListStatusSlider[2].value = Currentvalue2;
    }
    protected void FixedUpdate()
    {
        this.SliderStatus();
    }
}
