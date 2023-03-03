using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class PersistentProperty<TPropertyType>
{
    [SerializeField] protected TPropertyType _value;  //Для инспектроа
    protected TPropertyType _stored;  //Значение записаное на диске

    private TPropertyType _defaultValue;

    public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);
    public event OnPropertyChanged onChanged;

    public IDisposable Subscribe(OnPropertyChanged call)
    {
        onChanged += call;
        return new ActionDisposable(()=>onChanged-=call);
    }

    public PersistentProperty(TPropertyType defualtValue)
    {
        _defaultValue = defualtValue;
    }

    public TPropertyType Value 
        {
            get => _stored;
            set  {
                var isEqual = _stored.Equals(value);
                if (isEqual) return;

                var oldValue = _value;
                Write(value);
                _stored = _value = value;
                onChanged?.Invoke(value, oldValue);
            }
        }
    protected void Init()
    {
        _stored = _value = Read(_defaultValue);
    }
    protected abstract void Write(TPropertyType value);
    protected abstract TPropertyType Read(TPropertyType defaultValue);

    public void Validate()
    {
        if (!_stored.Equals(_value)) Value = _value; 
    }
}
