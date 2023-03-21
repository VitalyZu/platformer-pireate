using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class StatWidget : MonoBehaviour, IItemRenderer<StatDef>
{
    [SerializeField] private Text _name;
    [SerializeField] private Image _icon;
    [SerializeField] private Text _currentValue;
    [SerializeField] private Text _increaseValue;
    [SerializeField] private ProgressBarWidget _progress;
    [SerializeField] private GameObject _selector;

    private GameSession _session;
    private StatDef _data;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        UpdateView();
    }

    private void UpdateView()
    {
        var StatModel = _session.StatsModel;

        _icon.sprite = _data.Icon;
        _name.text = LocalizationManager.I.Localaize(_data.Name);
        _currentValue.text = StatModel.GetValue(_data.Id).ToString(CultureInfo.InvariantCulture);

        var currentLevel = StatModel.GetCurrentLevel(_data.Id);
        var nextLevel = currentLevel + 1;
        var increaseValue = StatModel.GetValue(_data.Id, nextLevel);
        _increaseValue.text = increaseValue.ToString(CultureInfo.InvariantCulture);
        _increaseValue.gameObject.SetActive(increaseValue > 0);

        var maxLevels = DefsFacade.I.Player.GetStat(_data.Id).Levels.Length;
        _progress.SetProgress(currentLevel / (float)maxLevels);

        _selector.SetActive(StatModel.InterfaceSelection.Value == _data.Id);
    }

    public void SetData(StatDef data, int index)
    {
        _data = data;

        if (_session != null)
            UpdateView();


    }

    public void OnSelect()
    {
        _session.StatsModel.InterfaceSelection.Value = _data.Id;
    }
}
