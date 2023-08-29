using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel3 : TutorialLevel
{
    [SerializeField] protected RectTransform ShopGunPannel;
    [SerializeField] protected GunUIManager gunUIManager;
    protected void OnEnable()
    {
        this.StartCoroutine(this.Flow());
    }
    protected IEnumerator Flow()
    {
        yield return new  WaitUntil(predicate:()=>
        {
            if(TutorialUI.Instance == null) return false;
            return true;
        });
        TutorialUI.Instance.ActiveButton(5);
        PanelCtrl.Instance.ShowPanel("MainMenuPannel");
        PanelCtrl.Instance.HirePanel("GameplayPanel");
        yield return new WaitForSeconds(2f);
        TutorialUI.Instance.ActivePanel(3);
        yield return new WaitUntil(predicate:()=>
        {
            if(DataManager.Instance.CurrentGunName == "Shotgun") return true;
            return ShopGunPannel.gameObject.activeInHierarchy;
        });
        TutorialUI.Instance.DeActivePanel();
        yield return new WaitForSeconds(1f);
        TutorialUI.Instance.ActivePanel(4);
        yield return new WaitUntil(predicate:()=>
        {
           if(gunUIManager.Index != 1) return false;
           return true; 
        });
        TutorialUI.Instance.DeActivePanel();
        TutorialUI.Instance.ActivePanel(5);
        yield return new WaitForSeconds(2f);
        yield return new WaitUntil(predicate:()=>
        {
           if(DataManager.Instance.CurrentGunName != "Shotgun") return false;
           return true; 
        });
        TutorialUI.Instance.DeActivePanel();
        yield return new WaitForSeconds(0.5f);
        TutorialUI.Instance.ActivePanel(7);
        yield return new WaitUntil(predicate:()=>
        {
            return !ShopGunPannel.gameObject.activeInHierarchy;
        });
        TutorialUI.Instance.DeActivePanel();
        DataManager.Instance.TutorialLevel = 3;
    }   
}
