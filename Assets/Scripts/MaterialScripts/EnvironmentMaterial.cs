using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMaterial : ChanceMaterials
{
    protected override bool CanChangce()
    {
        EnvironmentReciver environmentReciver = GetComponent<EnvironmentReciver>();
        if(environmentReciver == null) return false;
        return environmentReciver.Camblock;
    }
}
