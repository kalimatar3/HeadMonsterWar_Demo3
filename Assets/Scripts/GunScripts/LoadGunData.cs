using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGunData : MyBehaviour
{
    [SerializeField] protected GunsSO gunsSO;
    public GunsSO GunsSO => gunsSO ;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadGunSO();
    }
    protected void LoadGunSO()
    {
        if(GunsSO != null) return;
        string rePath = "Guns/" + transform.name;
        this.gunsSO = Resources.Load<GunsSO>(rePath);
        Debug.LogWarning( this.transform.name  + " LoadSO");
    }
}
