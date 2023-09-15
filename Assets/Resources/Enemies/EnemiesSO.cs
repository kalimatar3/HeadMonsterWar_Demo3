using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemie",menuName = "ScriptableObjects/Enemie")]
public class EnemiesSO : ScriptableObject
{
    public string EnemiesName = "Enemie";
    public string SkillName ;
    public int maxhp = 5;
    public float Dame = 1;
    public List<float> ListDroprate;
}