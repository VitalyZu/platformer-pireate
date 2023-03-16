using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
