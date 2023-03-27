using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    private readonly List<object> _retained = new List<object>();

    public bool IsLocked => _retained.Count > 0;

    public void Retain(object item)
    {
        _retained.Add(item);
    }
    public void Release(object item)
    {
        _retained.Remove(item);
    }
}
