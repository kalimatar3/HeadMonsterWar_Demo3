using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                PanelCtrl.Instance.ShowPanel("GameOverPanel");
            }
        } 
    }
    public override void ReBorn()
    {
        this.StartCoroutine(this.RebornDelay());
    }
    protected IEnumerator RebornDelay()
    {
        yield return new WaitUntil(predicate:()=>
        {
            if(DataManager.Instance.ListUpGradeAbleData == null) return false;
            return true;
        });
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
               if(!DataManager.Instance.GamePlayMode) base.DeductHp(10);
                else base.DeductHp(dame);
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
