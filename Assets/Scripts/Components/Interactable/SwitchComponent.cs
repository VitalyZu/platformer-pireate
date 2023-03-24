using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchComponent : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] bool _state;
    [SerializeField] private string _animationKey;
    [SerializeField] private bool _switchOnStart;

    private void Start()
    {
        if (_switchOnStart)
        {
            _animator.SetBool(_animationKey, _state);
        }    
    }
    private void Switch()
    {
        _state = !_state;
        _animator.SetBool(_animationKey, _state);
    }

    [ContextMenu("SwitchIt")]
    public void SwitchIt()
    {
        Switch();
    }
}
