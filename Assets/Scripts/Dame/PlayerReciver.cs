using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReciver : DameReciver
{
    public PlayerController playerController;
    [SerializeField] protected float InvulnerableNumber;
    public float CurinvulnerableNumber;
    [SerializeField] public bool CanRevise;
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
    protected override void Dying()
    {
        if(this.CurrentHp <= 0)
        {
            HolderManager.Instance.DeActiveHolder("SoundHolder");
            PanelCtrl.Instance.ShowPanel("GameOverPanel");
            PanelCtrl.Instance.HirePanel("GameplayPanel");
                if(!PanelGate)
                {
                    PanelGate = true;
                }
        }   
    }
    public override void ReBorn()
    {
        this.StartCoroutine(this.RebornDelay());
        StartCoroutine(this.TakeDameDelay());
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
        if(CanTakeDame)
        {
            SoundSpawner.Instance.Spawn(CONSTSoundsName.Attacked,Vector3.zero,Quaternion.identity);
            if(CurinvulnerableNumber > 0)
            {
                CurinvulnerableNumber --;
            }
            else
            {
               Transform panel = TakeDamePanelSpawner.Instance.Spawn(TakeDamePanelSpawner.Instance.TakeDamePanel,Vector3.zero,Quaternion.identity);
               panel.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
               panel.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
               if(!DataManager.Instance.GamePlayMode) base.DeductHp(10);
                else base.DeductHp(dame);
            }
        }
    }
    protected IEnumerator TakeDameDelay()
    {
        timer = 0;
        CanTakeDame = false;
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
