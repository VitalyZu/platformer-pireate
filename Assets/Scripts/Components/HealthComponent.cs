using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private UnityEvent _onDamage;
    [SerializeField] private UnityEvent _onHeal;
    [SerializeField] private UnityEvent _onDie;

    public void DealHealth(int health)
    {
        _health += health;
        Debug.Log(_health);

        if (health < 0)
        {
            _onDamage?.Invoke();
        }
        else
        {
            _onHeal?.Invoke();
        }
        
        if (_health <= 0)
        {
            _onDie?.Invoke();
        }
    }
}
