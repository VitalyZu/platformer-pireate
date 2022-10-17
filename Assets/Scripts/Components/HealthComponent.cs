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

    private Hero _hero;
    private bool _isHit;

    private void Awake()
    {
        _hero = gameObject.GetComponent<Hero>();
        
    }

    public void DealHealth(int health)
    {
        _isHit = _hero.isHit;
        if (_isHit) return;

        _health += health;
        Debug.Log(_health);

        if (health < 0 && !_isHit)
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
