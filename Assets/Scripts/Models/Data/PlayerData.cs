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

    public IntProperty HP = new IntProperty();
    public PerksData Perks = new PerksData();
    public LevelData Levels = new LevelData();

    //public object Clone()
    //{
    //    return this.MemberwiseClone();
    //}
    public PlayerData Clone()
    {
        var json = JsonUtility.ToJson(this);
        return JsonUtility.FromJson<PlayerData>(json);
    }
}
    

