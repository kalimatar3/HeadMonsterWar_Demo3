using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Guns",menuName = "ScriptableObjects/Guns")]
public class GunsSO : ScriptableObject
{
    public string GunName = "Pistol";
    public string Bulletname = "Pistolbullet";
    public bool CanThroughObj = false;
    public string HitExplosionName,FireExplosionName;
    public List<ClassData.GunData> GunUpgrade;
}
