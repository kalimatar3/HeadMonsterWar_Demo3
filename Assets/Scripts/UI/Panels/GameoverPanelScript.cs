using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class GameoverPanelScript : MyBehaviour
{
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
            color = Color.black;
            color.a = 0.9f;
            BackGround.color = Color.Lerp(BackGround.color,color,Time.deltaTime * 1f * 10f/1.5f);
            if(BackGround.color == color) return true;
            return false;
        });
        for(int i = 0 ; i < ListObjDelayAppear.Count ; i++)
        {
            ListObjDelayAppear[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }
    protected void OnEnable()
    {
        this.StartCoroutine(this.DelayAppearObj());
        SoundSpawner.Instance.Spawn(CONSTSoundsName.GameOver,Vector3.zero,Quaternion.identity);
        PanelCtrl.Instance.HirePanel("GameplayPanel");
        PanelCtrl.Instance.HirePanel("RevisePannel");
    }
    protected void OnDisable()
    {
        Color basecolor = Color.black;
        basecolor.a = 0f;
        this.BackGround.color = basecolor;
    } 
}
