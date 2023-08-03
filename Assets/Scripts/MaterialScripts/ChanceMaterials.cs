using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChanceMaterials : MyBehaviour
{
    [SerializeField] protected Material DefaultMaterial,NewMaterial;
    [SerializeField] MeshRenderer thisren;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRenderer();
        this.LoadDefaultMaterial();
    }
    protected void LoadRenderer()
    {
        thisren = GetComponent<MeshRenderer>();
    }
    protected void LoadDefaultMaterial()
    {
        this.DefaultMaterial = thisren.materials[0];
    }
    protected abstract bool CanChangce();
    protected void Chancing()
    {
        if(!CanChangce()) 
        {
            thisren.sharedMaterial = DefaultMaterial;
            return;
        }
        this.ChanceMaterial();
    }
    protected virtual void ChanceMaterial()
    {
        thisren.sharedMaterial = NewMaterial;
    }
    protected void FixedUpdate()
    {
        this.Chancing();
    }
}
