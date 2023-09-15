using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Cost",menuName = "ScriptableObjects/CostSO")]
public class CostSO : ScriptableObject
{
    public List<CostData> List;
}
[Serializable]
public struct CostData
{
    [SerializeField] public string Name;
    [SerializeField] public List<int> Cost;  
}
