using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CompositeDisposable : IDisposable
{
    private readonly List<IDisposable> _disposables = new List<IDisposable>();
    public void Retain(IDisposable dispose)
    {
        _disposables.Add(dispose);
    }
    public void Dispose()
    {

        foreach (var item in _disposables)
        {
            item.Dispose();
        }
        _disposables.Clear();
    }
}
