﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private Vector2 _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction.x, _rigidbody.velocity.y);
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void SaySomething()
    {
        Debug.Log("Say something");
    }
}
