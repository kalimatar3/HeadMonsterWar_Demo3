using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerSO",menuName = "ScriptableObjects/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public List<ClassData.PlayerData> PlayerUpgrade; 
}
