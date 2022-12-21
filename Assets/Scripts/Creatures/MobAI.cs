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

    private Coroutine _current;
    private GameObject _target;

    private SpawnListComponent _particles;
    private Creature _creature;

    private void Awake()
    {
        _particles = GetComponent<SpawnListComponent>();
        _creature = GetComponent<Creature>();
    }

    private void Start()
    {
        StartState(Patrolling());
    }

    private void StartState(IEnumerator coroutine)
    {
        if (_current != null) StopCoroutine(_current);
        
        _current = StartCoroutine(coroutine);
    }

    public void OnEnterVision(GameObject go)
    {
        _target = go;
        StartState(AgroToHero());
    }

    private void SetDirectionToTarget()
    {
        var direction = _target.transform.position - transform.position;
        direction.y = 0;
        _creature.SetDirection(direction.normalized);
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
