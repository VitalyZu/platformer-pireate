using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PerksData
{
    [SerializeField] private StringProperty _used = new StringProperty(); //используемый перк
    [SerializeField] private List<string> _unlocked; //список разблокированных паерков

    public StringProperty Used => _used;

    public void AddPerk(string id)
    {
        if (!_unlocked.Contains(id))
            _unlocked.Add(id);
    }

    public bool IsUnlocked(string id)
    {
        return _unlocked.Contains(id);
    }
}
