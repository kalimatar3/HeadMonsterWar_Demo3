using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnbyObj : Despawn
{
    [SerializeField] public Transform Obj;
    protected override bool CanDeSpawn()
    {
        if(Obj == null) return false;
        else    if(Obj.gameObject.activeInHierarchy) return false;
        return true;
    }
}
