using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMaterial : ChanceMaterials
{
    protected override bool CanChangce()
    {
        EnvironmentReciver environmentReciver = GetComponentInParent<EnvironmentReciver>();
        if(environmentReciver == null) return false;
        return environmentReciver.Camblock;
    }
}
