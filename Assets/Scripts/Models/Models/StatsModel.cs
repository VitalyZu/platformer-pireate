using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class StatsModel : IDisposable
{
    public readonly ObservableProperty<StatId> InterfaceSelection = new ObservableProperty<StatId>();

    private PlayerData _data;

    public event Action OnChanged;
    public event Action<StatId> OnUpgraded;
    private readonly CompositeDisposable _trash = new CompositeDisposable();
    
    public IDisposable Subscribe(Action call)
    {
        OnChanged += call;
        return new ActionDisposable( () => OnChanged -= call );
    }
    
    public StatsModel(PlayerData data)
    {
        _data = data;
        //InterfaceSelection.Value = DefsFacade.I.Player.Stats[0].Id;
        _trash.Retain(InterfaceSelection.Subscribe( (_, __) => OnChanged?.Invoke() ));
    }

    public void LevelUp(StatId id)
    {
        var def = DefsFacade.I.Player.GetStat(id);
        var nextLevel = GetCurrentLevel(id) + 1;

        if (def.Levels.Length <= nextLevel) return;

        var price = def.Levels[nextLevel].Price;
        
        if (!_data.Inventory.IsEnough(price)) return;

        _data.Inventory.RemoveItem(price.ItemId, price.Count);
        _data.Levels.LevelUp(id);

        OnChanged?.Invoke();
        OnUpgraded?.Invoke(id);
    }

    public float GetValue(StatId id, int level = -1)
    {
        //var def = DefsFacade.I.Player.GetStat(id);
        //var level = def.Levels[GetLevel(id)];
        //return level.Value;
        return GetLevelDef(id, level).Value;
    }
    public StatLevelDef GetLevelDef(StatId id, int level = -1)
    {
        if (level == -1) level = GetCurrentLevel(id);

        var def = DefsFacade.I.Player.GetStat(id);
        if(def.Levels.Length > level)
            return def.Levels[level];
        return default;
    }
    //public int GetCurrentLevel(StatId id) => _data.Levels.GetLevel(id);
    public int GetCurrentLevel(StatId id)
    {
        return _data.Levels.GetLevel(id);
    }

    public void Dispose()
    {
        _trash.Dispose();
    }
}
