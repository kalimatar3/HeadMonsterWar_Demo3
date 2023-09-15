using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ShopSO",menuName = "ScriptableObjects/ShopSO")]
public class ShopSO : ScriptableObject
{
    public List<ShopData> ListShopdata;
}