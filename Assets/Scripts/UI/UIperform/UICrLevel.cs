using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICrLevel : UIperform
{
    protected override void PreformText()
    {
        base.PreformText();
       Text.text = "Level " + (DataManager.Instance.CurrentLevel);
    }
}
