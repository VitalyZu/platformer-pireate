using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private bool _usePool;

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        GameObject instance = _usePool ? 
            Pool.Instance.Get(_prefab, _target.position) : 
            SpawnUtils.Spawn(_prefab, _target.position);
        
        instance.transform.localScale = _target.lossyScale;
    }

    public void SetPrefab(GameObject prefab)
    {
        _prefab = prefab;
    }
}
