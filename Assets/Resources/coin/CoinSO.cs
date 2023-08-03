using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "coin",menuName = "ScriptableObjects/coin")]
public class CoinSO : ScriptableObject
{
    public List<ClassData.CoinData> ListCoinValue; 
}
