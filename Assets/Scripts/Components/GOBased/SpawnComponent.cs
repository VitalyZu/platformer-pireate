using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _prefab;

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        GameObject instance = Instantiate(_prefab, _target.position, Quaternion.identity);
        instance.transform.localScale = _target.lossyScale;
    }

    public void SetPrefab(GameObject prefab)
    {
        _prefab = prefab;
    }
}
