﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuickInventoryController : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private InventoryItemWidget _prefab;

    private GameSession _session;
    private InventoryItemData[] _inventory;
    private List<InventoryItemWidget> _createdItems = new List<InventoryItemWidget>();

    private readonly CompositeDisposable _trash = new CompositeDisposable();

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();

        Rebuild();
    }

    private void Rebuild()
    {
        _inventory = _session.Data.Inventory.GetAll();

        //create required items
        for (int i = _createdItems.Count; i < _inventory.Length; i++)
        {
            var item = Instantiate(_prefab, _container);
            _createdItems.Add(item);
        }

        //update data and activate
        for (int i = 0; i < _inventory.Length; i++)
        {
            _createdItems[i].SetData(_inventory[i], i);
            _createdItems[i].gameObject.SetActive(true);
        }

        //hide unused items
        for (int i = _inventory.Length; i < _createdItems.Count; i++)
        {
            _createdItems[i].gameObject.SetActive(false);
        }
    }
}
