using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrapAI : MonoBehaviour
{
    [SerializeField] private LayerCheck _vision;
    [Space]
    [Header("Melee")]
    [SerializeField] private Cooldown _meleeCooldown;
    [SerializeField] private CheckCircleOverlap _meleeAttack;
    [SerializeField] private LayerCheck _meleeCanAttack;
    [Header("Melee")]
    [SerializeField] private Cooldown _rangeCooldown;
    [SerializeField] private SpawnComponent _rangeAttack;

    private static int rangeKey = Animator.StringToHash("range");
    private static int meleeKey = Animator.StringToHash("melee");
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_vision.isTouchingLayer)
        {
            if (_meleeCanAttack.isTouchingLayer)
            {
                if (_meleeCooldown.IsReady)
                {
                    MeleeAttack();
                    return;
                }
            }

            if (_rangeCooldown.IsReady)
            {
                RangeAttack();
            }
        }
    }

    private void MeleeAttack()
    {
        _meleeCooldown.Reset();
        _animator.SetTrigger(meleeKey);
    }
    private void RangeAttack()
    {
        _rangeCooldown.Reset();
        _animator.SetTrigger(rangeKey);
    }

    public void OnMeleeAttck()
    {
        _meleeAttack.Check();
    }
    public void OnRangeAttck()
    {
        _rangeAttack.Spawn();
    }
}
