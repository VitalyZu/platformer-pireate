using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour
{
    [SerializeField] ProgressBarWidget _bar;

    private GameSession _gameSession;
    private ShowWindowComponent _windowComponent;
    private void Start()
    {
        _windowComponent = GetComponent<ShowWindowComponent>();

        _gameSession = FindObjectOfType<GameSession>();

        _gameSession.Data.HP.onChanged += OnHealthChanged;
        OnHealthChanged(_gameSession.Data.HP.Value, 0);
    }

    private void OnHealthChanged(int newValue, int oldValue)
    {
        var maxHealth = DefsFacade.I.Player.MaxHealth;
        var value = (float)newValue / maxHealth;
        _bar.SetProgress(value);
    }

    private void OnDestroy()
    {
        _gameSession.Data.HP.onChanged -= OnHealthChanged;
    }

    public void OnDebug()
    {
        _windowComponent.Show("UI/PlayerStatsWindow");
    }
}
