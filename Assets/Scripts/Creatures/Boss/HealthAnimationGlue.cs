using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAnimationGlue : MonoBehaviour
{
    [SerializeField] private HealthComponent _hp;
    [SerializeField] private Animator _animator;

    private readonly static int HealthKey = Animator.StringToHash("health");
    private readonly static CompositeDisposable _trash = new CompositeDisposable();

    private void Awake()
    {
        _trash.Retain(_hp._onChange.Subscribe(OnHealthChanged));
        OnHealthChanged(_hp.Health);
    }

    private void OnHealthChanged(int health)
    {
        _animator.SetInteger(HealthKey, health);
    }

    private void OnDestroy()
    {
        _trash.Dispose();
    }
}
