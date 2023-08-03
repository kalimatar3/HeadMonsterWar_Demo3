using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIperform : MyBehaviour
{
    [SerializeField] protected Text Text;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }
    protected void LoadText()
    {
        this.Text = GetComponent<Text>();
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
