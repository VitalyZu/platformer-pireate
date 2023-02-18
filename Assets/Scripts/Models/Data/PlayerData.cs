using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData 
{
    [SerializeField] private InventoryData _inventory;

    public InventoryData Inventory => _inventory;
    [Space(30)]
    public int HP;

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
    

