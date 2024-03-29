﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizeText : MonoBehaviour
{
    [SerializeField] private string _key;
    [SerializeField] private bool _capitalizeText;
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
        LocalizationManager.I.OnLocaleChanged += OnLocaleChanged;
        Localize();
    }

    private void OnLocaleChanged()
    {
        Localize();
    }

    private void Localize()
    {
        var localized = LocalizationManager.I.Localaize(_key);
        _text.text = _capitalizeText ? localized.ToUpper() : localized;
    }

    private void OnDestroy()
    {
        LocalizationManager.I.OnLocaleChanged -= OnLocaleChanged;
    }
}
