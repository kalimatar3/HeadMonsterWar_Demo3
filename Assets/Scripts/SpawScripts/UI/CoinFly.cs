using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinFly : MyBehaviour
{
    public Vector3 StartPos,EndPos,SpawnPos;
    [SerializeField] protected float SpeedtoStart,SpeedtoEnd;
    protected void OnEnable()
    {
        if(StartPos == null) return;
        if(EndPos == null) return;
        this.StartCoroutine(Fly());
    }
    protected IEnumerator Fly()
    {        
        yield return new WaitForSeconds(0.5f);
            this.transform.parent.DOMove(this.StartPos,(StartPos-SpawnPos).magnitude/SpeedtoStart);
            yield return new WaitForSeconds((StartPos-SpawnPos).magnitude/SpeedtoStart);
            this.transform.parent.DOMove(this.EndPos,(EndPos-StartPos).magnitude/SpeedtoEnd);
            yield return new WaitForSeconds((EndPos-StartPos).magnitude/SpeedtoEnd);
            DataManager.Instance.IcrGold((int)(CoinUISpawner.Instance.CurrentNumberofCoins/5f));
            CoinUISpawner.Instance.DeSpawnToPool(this.transform.parent);

    }
}
