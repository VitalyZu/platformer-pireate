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

    public IDisposable Subscribe(OnPropertyChanged call)
    {
        onChanged += call;
        return new ActionDisposable(() => onChanged -= call);
    }
    public IDisposable SubscribeAndInvoke(OnPropertyChanged call)
    {
        onChanged += call;
        var dispose = new ActionDisposable(() => onChanged -= call);
        call(_value, _value);
        return dispose;
    }
    public TPropertyType Value 
    {
        get => _value;
        set {
            var isSame = _value?.Equals(value) ?? false;
            if (isSame) return;
            var oldValue = _value;
            _value = value;
            InvokeChangedEvent(_value, oldValue);
        }
    }

    protected void InvokeChangedEvent(TPropertyType newValue, TPropertyType oldValue)
    {
        onChanged?.Invoke(newValue, oldValue);
    }
}
