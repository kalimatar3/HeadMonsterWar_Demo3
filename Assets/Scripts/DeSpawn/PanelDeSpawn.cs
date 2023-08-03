using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelDeSpawn : Despawnbytime
{
    [SerializeField] protected Image image;
    [SerializeField] protected float timer;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImage();
    }
    protected void LoadImage()
    {
        image = GetComponent<Image>();
    }
    protected override void DeSpawnObjects()
    {
        StartCoroutine(DelayDespawn());
    }
    protected IEnumerator DelayDespawn()
    {
        yield return new WaitUntil(predicate: ()=>
        {
            Color color = new Color();
            color = Color.red;
            color.a = 0.5f;
            image.color = Color.Lerp(image.color,color,Time.deltaTime * 1f * 30f/1.5f);
            if(image.color == color) return true;
            return false;
        });
        image.color = Color.red;
        PanelCtrl.Instance.HirePanel(this.transform.name);
    }
}
