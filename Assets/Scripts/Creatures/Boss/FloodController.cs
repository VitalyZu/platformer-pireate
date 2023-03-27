using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _floodTime;

    private Coroutine _coroutine;
    private readonly static int _isFloodingKey = Animator.StringToHash("is-flooding");
    public void StartFlooding()
    {
        if (_coroutine != null) return;
        _coroutine = StartCoroutine(Animate());
    }
    private IEnumerator Animate() 
    {
        _animator.SetBool(_isFloodingKey, true);
        yield return new WaitForSeconds(_floodTime);
        _animator.SetBool(_isFloodingKey, false);
        _coroutine = null;
    }
}
