using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class WinScript : MyBehaviour
{
    [SerializeField] protected List<Transform> ListObjDelayAppear;
    protected void OnEnable()
    {
        this.StartCoroutine(this.DelayAppearObj());
        SoundSpawner.Instance.Spawn(CONSTSoundsName.Win,Vector3.zero,Quaternion.identity);
        PanelCtrl.Instance.HirePanel("GameplayPanel");
    }
    protected IEnumerator DelayAppearObj()
    {
        foreach(Transform ele in ListObjDelayAppear)
        {
            ele.gameObject.SetActive(false);
        }
        for(int i = 0 ; i < ListObjDelayAppear.Count ; i++)
        {
            ListObjDelayAppear[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }

}
