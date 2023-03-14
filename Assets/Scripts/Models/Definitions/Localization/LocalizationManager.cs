using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static readonly LocalizationManager I;

    private StringPersistentProperty _localeKey = new StringPersistentProperty("en", "localization/current");
    private Dictionary<string, string> _localization;

    public string LocaleKey => _localeKey.Value;

    public event Action OnLocaleChanged;
    static LocalizationManager()
    {
        I = new LocalizationManager();
    }
    public LocalizationManager()
    {
        LoadLocale(_localeKey.Value);
    }

    private void LoadLocale(string localeToLoad)
    {
        var def = Resources.Load<LocaleDef>($"Locales/{localeToLoad}");
        _localization = def.GetData();
        OnLocaleChanged?.Invoke();
    }

    public string Localaize(string key)
    {
        if (_localization.TryGetValue(key, out var value))
        {
            return value;
        }
        return $"%%%{key}%%%";
    }

    public void SetLocale(string selectedLocale)
    {
        LoadLocale(selectedLocale);
    }
}
