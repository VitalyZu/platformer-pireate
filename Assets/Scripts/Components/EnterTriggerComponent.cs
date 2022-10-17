using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterTriggerComponent : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private UnityEvent _action;
    [SerializeField] private PlayerEvent _collisionAction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_tag))
        {
            _action?.Invoke();
            _collisionAction?.Invoke(collision.gameObject);
        }
    }
}

[Serializable]
public class PlayerEvent : UnityEvent<GameObject>
{ }
