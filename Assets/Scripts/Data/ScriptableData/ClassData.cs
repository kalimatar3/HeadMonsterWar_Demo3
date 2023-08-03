using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class ClassData
{ 
    [Serializable]
    public class DropDta
    {
        public string Name;
        public float Quality;
    }
    [Serializable]
    public class GunData
    {
        public int Cost = 1;
        public float Firerate = 0.5f;
        public int MaxAmmo =  10; 
        public float Reloadtime = 1;
    
    }
    [Serializable]
    public class PlayerData
    {
        public int MaxHp;
    }
    [Serializable]
    public class CoinData
    {
        public int number;
    }
}  
