using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Cost",menuName = "ScriptableObjects/CostSO")]
public class CostSO : ScriptableObject
{
    [SerializeField] public string Name;
    [SerializeField] public List<int> Cost; 
}
