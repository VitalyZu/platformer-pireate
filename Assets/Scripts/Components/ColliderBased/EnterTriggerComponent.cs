using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterTriggerComponent : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private LayerMask _layer = ~0;
    [SerializeField] private UnityEvent _action;
    [SerializeField] private PlayerEvent _collisionAction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.IsInLayer(_layer)) return;
        if (!string.IsNullOrEmpty(_tag) && !collision.CompareTag(_tag)) return;
        
        _action?.Invoke();
        _collisionAction?.Invoke(collision.gameObject);
        
    }
}

[Serializable]
public class PlayerEvent : UnityEvent<GameObject>
{ }
