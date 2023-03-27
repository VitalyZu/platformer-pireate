using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPWidget : MonoBehaviour
{
    [SerializeField] private HealthComponent _health;
    [SerializeField] private ProgressBarWidget _bar;
    [SerializeField] private CanvasGroup _canvas;

    private readonly CompositeDisposable _trash = new CompositeDisposable();

    private float _maxHealth;

    private void Start()
    {
        _maxHealth = _health.Health;
        _trash.Retain(_health._onChange.Subscribe(OnHealthChanged));
    }

    private void OnHealthChanged(int hp)
    {
        _bar.SetProgress(_maxHealth / hp);
    }

    [ContextMenu("Show")]
    private void ShowUI()
    {
        this.LerpAnimated(0, 1, 1, SetAlpha);
    }
    [ContextMenu("Hide")]
    private void HideUI()
    {
        this.LerpAnimated(1, 0, 1, SetAlpha);
    }

    private void SetAlpha(float alpha)
    {
        _canvas.alpha = alpha;
    }
    private void OnDestroy()
    {
        _trash.Dispose();
    }
}
