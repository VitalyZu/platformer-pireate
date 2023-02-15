using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MobAI : MonoBehaviour
{
    [SerializeField] LayerCheck _vision;
    [SerializeField] LayerCheck _canAttack;
    [SerializeField] float _agroTime = .5f;
    [SerializeField] float _attackCooldown = 1f;
    [SerializeField] float _missCooldown = 1f;

    private IEnumerator _current;
    //private Coroutine _current;
    private GameObject _target;

    private SpawnListComponent _particles;
    private Creature _creature;
    private Animator _animator;
    private Patrol _patrol;

    private bool _isDead;

    private static readonly int dieKey = Animator.StringToHash("die");

    private void Awake()
    {
        _particles = GetComponent<SpawnListComponent>();
        _creature = GetComponent<Creature>();
        _animator = GetComponent<Animator>();
        _patrol = GetComponent<Patrol>();
    }

    private void Start()
    {
        StartState(_patrol.DoPatrol());
    }

    private void StartState(IEnumerator coroutine)
    {
        //if (_isDead) return;
        _creature.SetDirection(Vector2.zero);

        if (_current != null)
        {
            StopCoroutine(_current);
        }
        _current = coroutine;
        StartCoroutine(coroutine);
        //_current = StartCoroutine(coroutine);

    }

    public void OnEnterVision(GameObject go)
    {
        if (_isDead) return;

        _target = go;
        StartState(AgroToHero());
    }

    private void SetDirectionToTarget()
    {
        var direction = _target.transform.position - transform.position;
        direction.y = 0;
        _creature.SetDirection(direction.normalized);
    }

    private Vector2 GetDirectionToTarget()
    {
        var direction = _target.transform.position - transform.position;
        direction.y = 0;
        return direction.normalized;
    }

    public void OnDie()
    {
        _creature.SetDirection(Vector2.zero);

        _animator.SetBool(dieKey, true);
        _isDead = true;
        if (_current != null) StopCoroutine(_current);
        //StopAllCoroutines();
    }

    private IEnumerator AgroToHero()
    {
        LookAtHero();
        _particles.Spawn("Exclamation");
        yield return new WaitForSeconds(_agroTime);
        
        StartState(GoToHero());
    }
    private void LookAtHero()
    {
        _creature.SetDirection(Vector2.zero);

        var direction = GetDirectionToTarget();
        _creature.SetSpriteDirection(direction);
    }
    private IEnumerator GoToHero()
    {
        //yield return null;
        while (_vision.isTouchingLayer)
        {
            if (_canAttack.isTouchingLayer)
            {
                //yield return null;
                StartState(Attack());
            }
            else 
            {
                SetDirectionToTarget();
            }
            yield return null;
        }

        _creature.SetDirection(Vector2.zero);
        _particles.Spawn("Miss");

        yield return new WaitForSeconds(_missCooldown);

        StartState(_patrol.DoPatrol());
    }

    private IEnumerator Attack()
    {
        while (_canAttack.isTouchingLayer)
        {
            _creature.Attack();
            yield return new WaitForSeconds(_attackCooldown);
        }
        
        StartState(GoToHero());     
    }

    [ContextMenu("StopAllRoute")]
    public void StopAllRoute()
    {
        if (_current != null)
        {
            StopCoroutine(_current);
        }
    }
}
