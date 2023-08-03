using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvulnerableEffectDespawn : EffectDespawn
{
    protected override bool CanDeSpawn()
    {
        if(PlayerController.Instance.PlayerReciver.CurinvulnerableNumber > 0) return false;
        else return true;
    }
}
