using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UICurrentLevel : TextMeshPerform
{
    protected override void PreformText()
    {
        base.PreformText();
        Text.text = "Level " + (DataManager.Instance.CurrentLevel);
    }
}
