using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshPerform : MyBehaviour
{
    [SerializeField] protected TextMeshPro Text;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }
    protected void LoadText()
    {
        this.Text = GetComponent<TextMeshPro>();
        if(Text == null) return;
    }
    protected virtual void PreformText()
    {
    }
    protected virtual void FixedUpdate()
    {
        this.PreformText();
    }

}
