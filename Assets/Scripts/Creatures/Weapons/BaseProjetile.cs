using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjetile : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] private bool _invert;

    protected Rigidbody2D Rigidbody;
    protected int Direction;

    protected virtual void Start()
    {
        Direction = transform.lossyScale.x > 0 ? 1 : -1;
        Direction = _invert ? Direction * -1 : Direction;
        Rigidbody = GetComponent<Rigidbody2D>();
        var force = new Vector2(_speed * Direction, 0);
        Rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    //private void FixedUpdate()
    //{
    //    var position = _rb.position;
    //    position.x += _speed * _direction;
    //    _rb.MovePosition(position);
    //}
}
