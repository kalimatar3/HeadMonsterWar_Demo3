using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerReciver : DameReciver
{
    public PlayerController playerController;
    [SerializeField] protected float InvulnerableNumber;
    public float CurinvulnerableNumber;
    [SerializeField] protected bool CanRevise;
    [SerializeField] protected float timer,delaytime;
    protected bool PanelGate,CanTakeDame;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
        CanRevise = true;
    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(this.TakeDameDelay());
    }
    protected void LoadPlayerCtrl()
    {
        if(playerController != null) return;
        playerController = GetComponentInParent<PlayerController>();
        if(playerController == null ) Debug.LogWarning(this.transform + "can find playerCtrl");
    }
    public void canRevise()
    {
        CanRevise = false;
    }
    protected override void Dead()
    {
        if( this.CurrentHp <= 0)
        {
            if(CanRevise)
            {
                if(!PanelGate)
                {
                    PanelGate = true;
                    HolderManager.Instance.DeActiveHolder("SoundHolder");
                    PanelCtrl.Instance.ShowPanel("RevivePannel");
                    PanelCtrl.Instance.HirePanel("GameplayPanel");
                }
            }
            if(!CanRevise)
            {
                PanelCtrl.Instance.HirePanel("GameplayPanel");
                this.Replay();
            }
        } 
    }
    public void Replay()
    {
        StartCoroutine(DelayReplayByGameOver());
    }
    protected void Replaying()
    {
      ScenesManager.Instance.LoadScene(SceneManager.GetActiveScene().name);
      this.StartCoroutine(MapManager.Instance.DelayLoadMap());
    }
    protected IEnumerator DelayReplayByGameOver()
    {
        SoundSpawner.Instance.Spawn(CONSTSoundsName.GameOver,Vector3.zero,Quaternion.identity);
        PanelCtrl.Instance.HirePanel("RevisePannel");
        PanelCtrl.Instance.ShowPanel("GameOverPanel");
        yield return new WaitForSeconds(3f);
        this.Replaying();
    } 

    public override void  ReBorn()
    {
        this.MaxHp = playerController.playerSO.PlayerUpgrade[DataManager.Instance.GetUpgradenumberfromUGAD("Hp")].MaxHp;
        base.ReBorn();
    }
    public virtual void invulnerable()
    {
        CurinvulnerableNumber = InvulnerableNumber;  
    }
    public override void DeductHp(float dame)
    {
        SoundSpawner.Instance.Spawn(CONSTSoundsName.Attacked,Vector3.zero,Quaternion.identity);
        if(CanTakeDame)
        {
            CanTakeDame = false;
            timer = 0;
            if(CurinvulnerableNumber > 0)
            {
                CurinvulnerableNumber --;
            }
            else
            {
                PanelCtrl.Instance.ShowPanel("TakeDamePanel");
                base.DeductHp(dame);
            }
            StartCoroutine(TakeDameDelay());
        }
    }
    protected IEnumerator TakeDameDelay()
    {
        while(CanTakeDame == false)
        {
            yield return timer += Time.deltaTime * 1f;
            if(timer > delaytime) 
            {
                CanTakeDame = true;
            }
        }
    }
}
