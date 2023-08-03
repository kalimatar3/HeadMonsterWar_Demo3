using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpeedUp : BufftoPlayer
{
    protected static SpeedUp instance;
    public static SpeedUp Instance { get => instance;}

    [SerializeField] protected float SpeedUptime;
    protected override void Awake()
    {
        base.Awake();
        if(instance != null && instance != this)
        {
        }
        else instance = this;
    }
    protected void OnEnable()
    {
       // if(instance != this && instance.transform.parent.gameObject.activeInHierarchy) Destroy(this.transform.parent.gameObject);
    }

    protected override void SendDametoObj(Transform obj)
    {
        PlayerReciver playerReciver =  obj.transform.GetComponent<PlayerReciver>();
        if(playerReciver == null) return;
        playerReciver.playerController.PlayerMoving.BoostValue = dealnumber;
        playerReciver.playerController.PlayerMoving.BoostTime = SpeedUptime;

        if(BuffManager.Instance.CurrentBuffEffect != null)  EffectSpawner.Instance.DeSpawnToPool(BuffManager.Instance.CurrentBuffEffect);
        BuffManager.Instance.CurrentBuff = this.transform.parent;
        BuffManager.Instance.CurrentBuffEffect = EffectSpawner.Instance.Spawn("SpeedUpEffect",this.transform.parent.position,this.transform.parent.rotation);
        BuffManager.Instance.CurrentBuffEffect.GetComponentInChildren<EffectDespawn>().DespawnTime = SpeedUptime;

        SoundSpawner.Instance.Spawn(CONSTSoundsName.SpeedUp,Vector3.zero,Quaternion.identity);
        base.SendDametoObj(obj);
    }
}
