using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class StatsModel : IDisposable
{
    private PlayerData _data;

    public event Action OnChanged;
    private readonly CompositeDisposable _trash = new CompositeDisposable();
    
    public IDisposable Subscribe(Action call)
    {
        OnChanged += call;
        return new ActionDisposable(() => OnChanged -= call);
    }
    
    public StatsModel(PlayerData data)
    {
        _data = data;
    }

    public void LevelUp(StatId id)
    {
        var def = DefsFacade.I.Player.GetStat(id);
        var nextLevel = GetLevel(id) + 1;

        if (def.Levels.Length <= nextLevel) return;

        var price = def.Levels[nextLevel].Price;
        
        if (!_data.Inventory.IsEnough(price)) return;

        _data.Inventory.RemoveItem(price.ItemId, price.Count);
        _data.Levels.LevelUp(id);

        OnChanged?.Invoke();
    }

    public float GetValue(StatId id)
    {
        var def = DefsFacade.I.Player.GetStat(id);
        var level = def.Levels[GetLevel(id)];
        return level.Value;
    }

    public int GetLevel(StatId id) => _data.Levels.GetLevel(id);
    
    public void Dispose()
    {
        _trash.Dispose();
    }
}
