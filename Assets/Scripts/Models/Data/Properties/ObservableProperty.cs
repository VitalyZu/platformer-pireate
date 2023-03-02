using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ObservableProperty<TPropertyType>
{
    [SerializeField] TPropertyType _value;

    public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);
    public event OnPropertyChanged onChanged;
    public TPropertyType Value 
    {
        get => _value;
        set {
            var isEqual = _value.Equals(value);
            if (isEqual) return;
            var oldValue = _value;
            _value = value;
            onChanged?.Invoke(_value, oldValue);
        }
    }
}
