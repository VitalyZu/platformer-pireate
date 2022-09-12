using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private LayerCheck _layerCheck;

    [SerializeField] private float _groundRadius;
    [SerializeField] private Vector3 _groundDelta;

    private Rigidbody2D _rigidbody;
    private Vector2 _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);

        bool isJumping = _direction.y > 0;

        if (isJumping && IsGround())
        {
            _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void SaySomething()
    {
        Debug.Log("Say something");
    }

    public bool IsGround() 
    {
        //RaycastHit2D hit = Physics2D.CircleCast(transform.position + _groundDelta, _groundRadius, Vector2.down, 0, _groundMask);
        //return hit.collider != null;
        return _layerCheck.isTouchingLayer;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGround() ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position + _groundDelta, _groundRadius);
    }
}
