﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LocaleItemWidget : MonoBehaviour, IItemRenderer<LocaleInfo>
{
    [SerializeField] private Text _text;
    [SerializeField] private GameObject _selector;
    [SerializeField] private SelectLocale _onSelected;

    private LocaleInfo _data;

    private void Start()
    {
        LocalizationManager.I.OnLocaleChanged += UpdateSelection;
    }

    public void SetData(LocaleInfo localeInfo, int index)
    {
        _data = localeInfo;
        UpdateSelection();
        _text.text = localeInfo.LocaleId.ToUpper();
    }

    private void UpdateSelection()
    {
        var isSelected = LocalizationManager.I.LocaleKey == _data.LocaleId;
        _selector.SetActive(isSelected);
    }

    public void OnSelected()
    {
        _onSelected?.Invoke(_data.LocaleId);
    }
}

[Serializable]
public class SelectLocale : UnityEvent<string>
{}

public class LocaleInfo
{
    public string LocaleId;
}
