using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DropItem",menuName = "ScriptableObjects/DropItemSO")]
public class DropItemSO : ScriptableObject
{
    public string Name;
    public List<float> Quality;
}
