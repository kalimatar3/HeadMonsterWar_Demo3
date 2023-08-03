using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDealToEnvironment : DameDealer
{
    protected override void OnTriggerEnter(Collider other)
    {
        //if(other.transform.childCount <= 0) return;
        //if(!other.transform.GetChild(0).gameObject.activeInHierarchy) return ;
        EnvironmentReciver environmentReciver = other.GetComponent<EnvironmentReciver>();
        if(environmentReciver == null) return;
        environmentReciver.Camblock = true;
    }
    protected void OnTriggerExit(Collider other)
    {
        //if(other.transform.childCount <= 0) return;
        //if(other.transform.GetChild(0).gameObject.activeInHierarchy) return;
        EnvironmentReciver environmentReciver = other.GetComponent<EnvironmentReciver>();
        if(environmentReciver == null) return;
        environmentReciver.Camblock = false;
    }
}
