using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Bullets",menuName = "ScriptableObjects/Bullets")]
public class BulletsSO : ScriptableObject
{
    public float Dame = 1;
    public bool CanThroughObj = false;
}
