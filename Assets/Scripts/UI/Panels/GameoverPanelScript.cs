using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class GameoverPanelScript : MyBehaviour
{
    [SerializeField] protected Transform ReviveButton;
    [SerializeField] protected Image BackGround;
    [SerializeField] protected List<Transform> ListObjDelayAppear;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBackground();
    }
    protected void LoadBackground()
    {
        this.BackGround = GetComponent<Image>();
    }
    protected IEnumerator DelayAppearObj()
    {
        foreach(Transform ele in ListObjDelayAppear)
        {
            ele.gameObject.SetActive(false);
        }
        yield return new WaitUntil(predicate: ()=>
        {
            Color color = new Color();
            color = Color.white;
            color.a = 0.9f;
            BackGround.color = Color.Lerp(BackGround.color,color,Time.deltaTime * 1f * 10f/1.5f);
            if(BackGround.color == color) return true;
            return false;
        });
        this.ReviveButton.gameObject.SetActive(PlayerController.Instance.PlayerReciver.CanRevise);
        for(int i = 0 ; i < ListObjDelayAppear.Count ; i++)
        {
            ListObjDelayAppear[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }
    protected void OnEnable()
    {
        SoundSpawner.Instance.Spawn(CONSTSoundsName.GameOver,Vector3.zero,Quaternion.identity);
        PanelCtrl.Instance.HirePanel("GameplayPanel");
        PanelCtrl.Instance.HirePanel("RevisePannel");
        this.StartCoroutine(this.DelayAppearObj());
    }
    protected void OnDisable()
    {
        Color basecolor = Color.white;
        basecolor.a = 0f;
        this.BackGround.color = basecolor;
    } 
}
