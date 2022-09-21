using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private LayerCheck _layerCheck;

    private Rigidbody2D _rigidbody;
    private Vector2 _direction;

    private int _coinsAmount = 0;
    private int _coinsValue = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);

        bool isJumping = _direction.y > 0;

        if (isJumping)
        {
            if (IsGround())
            {
                _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
            }

        }
        else if (_rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * .5f);
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
        return _layerCheck.isTouchingLayer;
    }

    public void setCoins(int coins)
    {
        _coinsValue++;
        _coinsAmount += coins;
        Debug.Log($"Coins: {_coinsValue} ({ _coinsAmount} $)");
    }
}
