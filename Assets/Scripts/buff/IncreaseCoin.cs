using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class IncreaseCoin : BufftoPlayer
{
    [SerializeField] protected Quaternion thisrotation;
    protected override void SendDametoObj(Transform obj)
    {
        PlayerReciver playerReciver =  obj.transform.GetComponent<PlayerReciver>();
        if(playerReciver == null) return;
        SoundSpawner.Instance.Spawn(CONSTSoundsName.PickCoin,Vector3.zero,Quaternion.identity);
        EffectSpawner.Instance.Spawn("PIckUpCoinEffect",this.transform.parent.position,Quaternion.identity);
        DataManager.Instance.IcrGold((int)dealnumber);
        StartCoroutine(DelayDeSpawn(obj));
    }
    protected IEnumerator DelayDeSpawn(Transform obj)
    {
        yield return new WaitUntil(predicate : () =>
        {
            Tweener tweener =  this.transform.DORotate(new Vector3(360,360,0),0.5f,RotateMode.FastBeyond360)
            .SetLoops(-1,LoopType.Restart)
            .SetRelative()
            .SetEase(Ease.Linear);
            this.transform.parent.Translate(Vector3.up * Time.deltaTime * 1f * 10);
            if(this.transform.parent.position.y < 7) return false;
            else 
            {
            this.transform.DORotate(new Vector3(90,0,0),0f,RotateMode.FastBeyond360)
            .SetLoops(-1,LoopType.Restart)
            .SetRelative()
            .SetEase(Ease.Linear);
                return true;
            }
        }); 
        base.SendDametoObj(obj);
    }
}
