using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public abstract class ChanceMaterials : MyBehaviour
{
    [SerializeField] protected Material NewMaterial;
    [SerializeField] protected Material[] DefaultMaterial,ListNewMaterial;
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
        DefaultMaterial = thisren.materials;
        ListNewMaterial =  new Material[DefaultMaterial.Length];
        for(int i = 0 ; i < thisren.materials.Length ; i++)
        {
           ListNewMaterial[i] = NewMaterial;
        }
    }
    protected override void Start()
    {
        base.Start();
    }
    protected abstract bool CanChangce();
    protected void Chancing()
    {
        if(!CanChangce()) 
        {
            thisren.materials = DefaultMaterial;
        return;
        }
        this.ChanceMaterial();
    }
    protected virtual void ChanceMaterial()
    {
        thisren.materials = ListNewMaterial; 
    }
    protected void FixedUpdate()
    {
        this.Chancing();
    }
}
