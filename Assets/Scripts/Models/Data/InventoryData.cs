﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class InventoryData
{
    [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();

    public delegate void OnInventoryChanged(string id, int value);
    public OnInventoryChanged OnChange;

    public bool IsEnough(params ItemWithCount[] items)
    {
        var joined = new Dictionary<string, int>();

        foreach (var item in items)
        {
            if (joined.ContainsKey(item.ItemId))
                joined[item.ItemId] += item.Count;
            else
                joined.Add(item.ItemId, item.Count);
        }

        foreach (var item in joined)
        {
            var count = Count(item.Key);
            if (count < item.Value)
                return false;
        }
        return true;
    }

    //public Action<string, int> OnChange;
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
        OnChange?.Invoke(id, Count(id));
    }

    public void RemoveItem(string id, int value)
    {
        var itemDefs = DefsFacade.I.Items.Get(id);
        if (itemDefs.IsVoid) return;

        var item = GetItemData(id);

        if (item == null) return;

        item.Value -= value;
        if(item.Value <= 0) _inventory.Remove(item);
        OnChange?.Invoke(id, Count(id));
    }

    private InventoryItemData GetItemData(string id)
    {
        foreach (var item in _inventory)
        {
            if (item.Id == id) return item;
        }
        return null;
    }

    public int Count(string id)
    {
        int count = 0;
        foreach (var item in _inventory)
        {
            if (item.Id == id) count += item.Value;
        }
        return count;
    }

    public InventoryItemData[] GetAll(params ItemTag[] tags)
    {
        var retVal = new List<InventoryItemData>();
        foreach (var item in _inventory)
        {
            var defs = DefsFacade.I.Items.Get(item.Id);
            bool HasAllTags = tags.All(x => defs.HasTag(x));
            if (HasAllTags) retVal.Add(item);
        }

        return retVal.ToArray();
    }
}

[Serializable]
public class InventoryItemData
{
    [InventoryId] public string Id;
    public int Value;

    public InventoryItemData(string id)
    {
        Id = id;
    }
}
