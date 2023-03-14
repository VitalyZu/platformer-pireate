using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationWindow : AnimatedWindow
{
    [SerializeField] private LocaleItemWidget _prefab;
    [SerializeField] private Transform _container;

    private readonly string[] _locales = {"ru", "en"};

    private DataGroup<LocaleInfo, LocaleItemWidget> _dataGroup;
    protected override void Start()
    {
        base.Start();
        _dataGroup = new DataGroup<LocaleInfo, LocaleItemWidget>(_prefab, _container);
        _dataGroup.SetData(ComposeData());
    }

    private List<LocaleInfo> ComposeData()
    {
        var data = new List<LocaleInfo>();

        foreach (var item in _locales)
        {
            data.Add(new LocaleInfo { LocaleId = item });
        }

        return data;
    }

    public void OnSelected(string selectedLocale)
    {
        LocalizationManager.I.SetLocale(selectedLocale);
    }
}
