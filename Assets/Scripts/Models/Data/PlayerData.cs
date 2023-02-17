using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData 
{
    [SerializeField] private InventoryData _inventory;

    public int Coins;
    public int CoinsAmount;
    public int Swords;
    public int HP;
    public bool IsArmed;

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
    

