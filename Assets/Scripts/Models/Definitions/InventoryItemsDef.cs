using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(menuName = "Defs/InventoryItem", fileName = "InventoryItem")]
public class InventoryItemsDef : ScriptableObject
{
    [SerializeField] private ItemDef[] _items;

    public ItemDef Get(string id)
    {
        foreach (var item in _items)
        {
            if (item.Id == id) return item;
        }
        return default;
    }
#if UNITY_EDITOR
    public ItemDef[] ItemsForEditor => _items;
#endif
    [Serializable]
    public struct ItemDef 
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private ItemTag[] _tags;

        public string Id => _id;
        public bool IsVoid => string.IsNullOrEmpty(_id);
        public Sprite Icon => _icon;

        public bool HasTag(ItemTag tag)
        {
            return _tags.Contains(tag); //LINQ
        }
    }
}
