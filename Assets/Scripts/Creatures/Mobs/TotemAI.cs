using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemAI : MonoBehaviour
{
    [SerializeField] private SpawnComponent _rangeAttack;

    private static int rangeKey = Animator.StringToHash("attack");
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void MakeAttack()
    {
        _animator.SetTrigger(rangeKey);
    }
    public void DoMakeAttack()
    {
        _rangeAttack.Spawn();
    }
}
