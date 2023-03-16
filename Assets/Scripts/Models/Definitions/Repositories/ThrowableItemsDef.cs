using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(menuName = "Defs/ThrowableItemsDef", fileName = "ThrowableItemsDef")]
public class ThrowableItemsDef : DefRepository<ThrowableDef>
{
}

[Serializable]
public struct ThrowableDef : IHaveId
{
    [InventoryId] [SerializeField] private string _id;
    [SerializeField] private GameObject _projectile;

    public string Id => _id;
    public GameObject Projectile => _projectile;
}
