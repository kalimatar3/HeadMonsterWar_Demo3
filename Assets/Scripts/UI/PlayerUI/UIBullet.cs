using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBullet : UIperform
{
    protected override void PreformText()
    {
        base.PreformText();
        StartCoroutine(this.PreformTextDelay());
    }
    protected IEnumerator PreformTextDelay()
    {
        yield return new WaitUntil(predicate:()=>
        {
            if(PlayerController.Instance.GunCtrl.Shooting == null) return false;
            return true;
        });
        this.Text.text = (PlayerController.Instance.GunCtrl.Shooting.CurrentAmmo.ToString() + "/" + PlayerController.Instance.GunCtrl.Shooting.MaxAmmo);
    }
}
