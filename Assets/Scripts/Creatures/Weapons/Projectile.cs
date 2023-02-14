using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;
    private int _direction;

    private void Start()
    {
        _direction = transform.lossyScale.x > 0 ? 1 : -1;
        _rb = GetComponent<Rigidbody2D>();
        var force = new Vector2(_speed * _direction, 0);
        _rb.AddForce(force, ForceMode2D.Impulse);
    }

    //private void FixedUpdate()
    //{
    //    var position = _rb.position;
    //    position.x += _speed * _direction;
    //    _rb.MovePosition(position);
    //}
}
