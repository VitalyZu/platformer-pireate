using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    }

    public void GetValue(StatId id)
    { 
        
    }

    public int GetLevel(StatId id) => _data.Levels.GetLevel(id);
    
    public void Dispose()
    {
        _trash.Dispose();
    }
}
