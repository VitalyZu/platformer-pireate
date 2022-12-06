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
    //private SpriteAnimation _spriteAnimationComponent;
    private bool _isHit;

    private void Awake()
    {
        _hero = gameObject.GetComponent<Hero>();
        //_spriteAnimationComponent = gameObject.GetComponent<SpriteAnimation>();
    }

    public void DealHealth(int health)
    {
        Debug.Log("Deal health");
        if (_hero != null)
        {
            _isHit = _hero.isHit;
        }
        else
        {
            _isHit = false;
        }
        /*else 
        {
            if (_spriteAnimationComponent != null)
            {
                if (_spriteAnimationComponent.animationName == "hit")
                {
                    _isHit = true;                  
                }
                else 
                {
                    _isHit = false;
                }
            }           
        }*/

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
