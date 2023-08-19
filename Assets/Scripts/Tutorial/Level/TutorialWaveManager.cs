using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWaveManager : MyBehaviour
{
    [SerializeField] protected List<Transform> ListWave;
    [SerializeField] protected int CurrentWave;
    [SerializeField] protected float preparetime;
    public float CoinLevelReward;
    protected float timer;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWaves();
    }
    protected void LoadWaves()
    {
        if(ListWave.Count != 0 ) return;
        foreach(Transform wave in this.transform)
        {
            ListWave.Add(wave);
        }
    }
    protected void OnEnable()
    {
        this.StartCoroutine(this.DelayLoadRewardCoin());
    }
    protected IEnumerator DelayLoadRewardCoin()
    {
        yield return new WaitUntil(predicate :()=>
        {
            if(CoinUISpawner.Instance == null) return false;
            return true;
        });
        CoinUISpawner.Instance.CurrentNumberofCoins = (int)this.CoinLevelReward;
    }
    protected virtual void ChangeLvInEmty()
    {
        foreach(Transform Wave in ListWave)
        {
            if(Wave.gameObject.activeInHierarchy)
            {
                TutorialLevel tutorialLevel = Wave.GetComponent<TutorialLevel>();
                if(tutorialLevel == null) return;
                if(tutorialLevel.IsDone == false) return;
            }
        }
        timer += Time.deltaTime * 1f;
        if(timer > preparetime)
        {
            timer = 0;
            foreach(Transform element in ListWave)  element.gameObject.SetActive(false);
            ListWave[CurrentWave].gameObject.SetActive(true);
            CurrentWave ++;
            if(CurrentWave >= ListWave.Count)
            {
                PanelCtrl.Instance.ShowPanel("Winpannel");
                TutorialLevelManager.Instance.NextLevel();
                this.transform.gameObject.SetActive(false);
            }
        }
    }
    protected void FixedUpdate()
    {
        this.ChangeLvInEmty();
    }

}
