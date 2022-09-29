using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private LayerCheck _layerCheck;

    private static readonly int isGroundKey = Animator.StringToHash("isGround");
    private static readonly int verticalVelocityKey = Animator.StringToHash("verticalVelocity");
    private static readonly int isRunningKey = Animator.StringToHash("isRunning");

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private Animator _animator;
    private Vector2 _direction;

    private int _coinsAmount = 0;
    private int _coinsValue = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);

        bool isJumping = _direction.y > 0;
        bool isGround = IsGround();

        if (isJumping)
        {
            if (isGround)
            {
                _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
            }

        }
        else if (_rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * .5f);
        }
        _animator.SetBool(isGroundKey, isGround);
        _animator.SetFloat(verticalVelocityKey, _rigidbody.velocity.y);
        _animator.SetBool(isRunningKey, _direction.x != 0);

        SetSpriteDirection();
    }

    private void SetSpriteDirection()
    {
        if (_direction.x > 0)
        {
            _sprite.flipX = true;
        }
        else if (_direction.x < 0)
        {
            _sprite.flipX = false;
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
