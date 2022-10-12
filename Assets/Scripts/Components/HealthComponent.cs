using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private UnityEvent _onDamage;
    [SerializeField] private UnityEvent _onDie;

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        _onDamage?.Invoke();
        if (_health <= 0)
        {
            _onDie?.Invoke();
        }
    }
}
