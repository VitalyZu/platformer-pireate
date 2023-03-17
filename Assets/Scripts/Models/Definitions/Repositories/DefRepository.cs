using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DefRepository<TDefType> : ScriptableObject where TDefType : IHaveId
{
    [SerializeField] protected TDefType[] _collection;

    public TDefType Get(string id)
    {
        foreach (var item in _collection)
        {
            if (item.Id == id) return item;
        }
        return default;
    }

    public TDefType[] All => new List<TDefType>(_collection).ToArray();

}
