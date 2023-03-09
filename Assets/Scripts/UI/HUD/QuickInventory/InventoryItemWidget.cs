using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemWidget : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _selected;
    [SerializeField] private Text _value;

    private GameSession _session;
    private int _index;
    private readonly CompositeDisposable _trash = new CompositeDisposable();

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        var index = _session.QuickInventory.SelectedIndex;
        _trash.Retain(index.SubscribeAndInvoke(OnIndexChanged));
    }

    private void OnIndexChanged(int newValue, int oldValue)
    {
        _selected.SetActive(_index == newValue);
    }

    public void SetData(InventoryItemData item, int index)
    {
        _index = index;
        var def = DefsFacade.I.Items.Get(item.Id);
        _icon.sprite = def.Icon;
        _value.text = item.Value.ToString();
    }

    private void OnDestroy()
    {
        _trash.Dispose();
    }
}
