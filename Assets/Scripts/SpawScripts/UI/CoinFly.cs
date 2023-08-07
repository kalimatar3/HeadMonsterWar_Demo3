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
        yield return new WaitForSeconds(1);
        if(CoinUIManager.Instance.CurrentCoinTextNumber > CoinUISpawner.Instance.CurrentNumberofCoins)
        {
            this.transform.parent.DOMove(this.StartPos,(StartPos-SpawnPos).magnitude/SpeedtoStart);
            yield return new WaitForSeconds((StartPos-SpawnPos).magnitude/SpeedtoStart);
            this.transform.parent.DOMove(this.EndPos,(EndPos-StartPos).magnitude/SpeedtoEnd);
            yield return new WaitForSeconds((EndPos-StartPos).magnitude/SpeedtoEnd);
            DataManager.Instance.IcrGold(5);
            CoinUISpawner.Instance.DeSpawnToPool(this.transform.parent);
        }
    }
}
