using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BloodSplashOverlay : MonoBehaviour
{
    [SerializeField] private Transform _overlay;

    private GameSession _session;
    private Animator _animator;
    private Vector3 _overScale;
    private readonly CompositeDisposable _trash = new CompositeDisposable();
    private static readonly int healthKey = Animator.StringToHash("health");

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        _animator = GetComponent<Animator>();
        _overScale = _overlay.localScale;

        _trash.Retain(_session.Data.HP.SubscribeAndInvoke(OnHpChange));
    }

    private void OnHpChange(int newValue, int _)
    {
        var maxHP = _session.StatsModel.GetValue(StatId.HP);
        var normalizedHP = newValue / maxHP;
        _animator.SetFloat(healthKey, normalizedHP);

        var overlayModifier = Mathf.Max(normalizedHP - 0.3f, 0f);
        _overlay.localScale = Vector3.one + _overScale * overlayModifier;
    }

    private void OnDestroy()
    {
        _trash.Dispose();
    }
}
