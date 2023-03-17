﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PerksModel : IDisposable
{
    private PlayerData _data;
    
    public readonly StringProperty InterfaceSelection = new StringProperty();
    public string Used => _data.Perks.Used.Value;

    public event Action OnChanged;
    private readonly CompositeDisposable _trash = new CompositeDisposable();

    public PerksModel(PlayerData data)
    {
        _data = data;
        InterfaceSelection.Value = DefsFacade.I.Perks.All[0].Id;

        _trash.Retain(_data.Perks.Used.Subscribe((x, y) => OnChanged?.Invoke()));
        _trash.Retain(InterfaceSelection.Subscribe((x, y) => OnChanged?.Invoke()));
    }

    public IDisposable Subscribe(Action call)
    {
        OnChanged += call;
        return new ActionDisposable(() => OnChanged -= call);
    }

    public void Unlock(string id)
    {
        var def = DefsFacade.I.Perks.Get(id);
        var isEnoughResourses = _data.Inventory.IsEnough(def.Price);

        if (isEnoughResourses)
        {
            _data.Inventory.RemoveItem(def.Price.ItemId, def.Price.Count);
            _data.Perks.AddPerk(id);
        }
        OnChanged?.Invoke();
    }

    public void UsePerk(string selectedPerk)
    {
        _data.Perks.Used.Value = selectedPerk;
    }

    public bool IsUsed(string id)
    {
        return _data.Perks.Used.Value == id;
    }

    public bool IsUnlocked(string id)
    {
        return _data.Perks.IsUnlocked(id);
    }

    public bool CanBuy(string selected)
    {
        var def = DefsFacade.I.Perks.Get(selected);
        return _data.Inventory.IsEnough(def.Price);
    }

    public void Dispose()
    {
        _trash.Dispose();
    } 
}
