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

    private Coroutine _current;
    private GameObject _target;

    private SpawnListComponent _particles;
    private Creature _creature;
    private Animator _animator;

    private bool _isDead;

    private static readonly int dieKey = Animator.StringToHash("die");

    private void Awake()
    {
        _particles = GetComponent<SpawnListComponent>();
        _creature = GetComponent<Creature>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartState(Patrolling());
    }

    private void StartState(IEnumerator coroutine)
    {
        _creature.SetDirection(Vector2.zero);

        if (_current != null) StopCoroutine(_current);
        
        _current = StartCoroutine(coroutine);
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

    public void OnDie()
    {
        _animator.SetBool(dieKey, true);
        _isDead = true;
        if (_current != null) StopCoroutine(_current);
    }

    private IEnumerator Patrolling()
    {
        yield return null;
    }

    private IEnumerator AgroToHero()
    {
        _particles.Spawn("Exclamation");
        yield return new WaitForSeconds(_agroTime);
        
        StartState(GoToHero());
    }

    private IEnumerator GoToHero()
    {
        while (_vision.isTouchingLayer)
        {
            if (_canAttack.isTouchingLayer)
            {
                StartState(Attack());
            }
            else 
            {
                SetDirectionToTarget();
            }
            
            yield return null;
        }
        //_creature.SetDirection(Vector2.zero);
        _particles.Spawn("Miss");

        yield return new WaitForSeconds(_missCooldown);
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
}
