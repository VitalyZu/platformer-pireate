using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterCollisionComponent : MonoBehaviour
{
    [SerializeField] private string[] _tag;
    [SerializeField] private EnterEvent _action;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var item in _tag)
        {
            if (collision.gameObject.CompareTag(item))
            {
                _action?.Invoke(collision.gameObject);
            }
        }       
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (var item in _tag)
        {
            if (collision.gameObject.CompareTag(item))
            {
                _action?.Invoke(collision.gameObject);
            }
        }
    }
}

[Serializable]
public class EnterEvent : UnityEvent<GameObject>
{}
