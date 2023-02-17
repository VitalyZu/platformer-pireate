using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventoryData : MonoBehaviour
{
    [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();

    public void AddItem(string id, int value)
    {
        if (value <= 0) return;

        var itemDefs = DefsFacade.I.Items.Get(id);
        if (itemDefs.IsVoid) return;

        var item = GetItemData(id);

        if (item == null)
        {
            item = new InventoryItemData(id);
            _inventory.Add(item);
            
        }
        item.Value += value;
    }

    public void RemoveItem(string id, int value)
    {
        var itemDefs = DefsFacade.I.Items.Get(id);
        if (itemDefs.IsVoid) return;

        var item = GetItemData(id);

        if (item == null) return;

        item.Value -= value;
        if(item.Value <= 0) _inventory.Remove(item);
    }

    private InventoryItemData GetItemData(string id)
    {
        foreach (var item in _inventory)
        {
            if (item.Id == id) return item;
        }
        return null;
    }

    public class InventoryItemData
    {
        public string Id;
        public int Value;

        public InventoryItemData(string id)
        {
            Id = id;
        }
    }
}
