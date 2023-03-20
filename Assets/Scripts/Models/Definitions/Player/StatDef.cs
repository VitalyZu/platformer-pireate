using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct StatDef
{
    [SerializeField] private StatId _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] StatLevel[] _levels;

    public StatId Id => _id;
    public string Name => _name;
    public Sprite Icon => _icon;
}

[Serializable]
public struct StatLevel
{
    [SerializeField] private float _value;
    [SerializeField] private ItemWithCount _price;

    public float Value => _value;
    public ItemWithCount Price => _price;
}

public enum StatId
{ 
    HP,
    Speed,
    RangeDamage
}
