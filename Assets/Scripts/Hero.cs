using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _damageJumpSpeed;
    [SerializeField] private LayerMask _groundMask;
    
    [SerializeField] private float _interactRadius;
    [SerializeField] private LayerMask _interactMask;
    private Collider2D[] _interactResult = new Collider2D[1];

    [SerializeField] private LayerCheck _layerCheck;

    private static readonly int isGroundKey = Animator.StringToHash("isGround");
    private static readonly int verticalVelocityKey = Animator.StringToHash("verticalVelocity");
    private static readonly int isRunningKey = Animator.StringToHash("isRunning");
    private static readonly int hitKey = Animator.StringToHash("hit");

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private Animator _animator;
    private Vector2 _direction;

    private bool _isGrounded;
    private bool _allowDoubleJump;

    private int _coinsAmount = 0;
    private int _coinsValue = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _isGrounded = IsGround();
    }

    private void FixedUpdate()
    {
        float xVelocity = _direction.x * _speed;
        float yVelocity = CalculateYVelocity();

        _rigidbody.velocity = new Vector2(xVelocity, yVelocity);

        
        _animator.SetBool(isGroundKey, _isGrounded);
        _animator.SetFloat(verticalVelocityKey, _rigidbody.velocity.y);
        _animator.SetBool(isRunningKey, _direction.x != 0);

        SetSpriteDirection();
    }

    private float CalculateYVelocity()
    {
        float yVelocity = _rigidbody.velocity.y;

        bool isJumping = _direction.y > 0;

        if (_isGrounded) _allowDoubleJump = true;

        if (isJumping)
        {
            yVelocity = CalculateJumpVelocity(yVelocity);
        }
        else if (_rigidbody.velocity.y > 0)
        {
            yVelocity *= .5f;
        }

        return yVelocity;
    }

    private float CalculateJumpVelocity(float velocity)
    {
        bool isFalling = _rigidbody.velocity.y <= .001f;

        if (!isFalling) return velocity;

        if (_isGrounded)
        {       
            velocity += _jumpSpeed;
        }
        else if (_allowDoubleJump) 
        {
            velocity = _jumpSpeed;
            _allowDoubleJump = false;
        }

        return velocity;
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

    public void GetDamage()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageJumpSpeed);
        _animator.SetTrigger(hitKey);
    }
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void SaySomething()
    {
        Debug.Log("Say something");
    }

    public void Interact()
    {
        int hit = Physics2D.OverlapCircleNonAlloc(transform.position, _interactRadius, _interactResult, _interactMask);
        for (int i = 0; i < hit; i++)
        {
            InteractableComponent interactComponent = _interactResult[i].GetComponent<InteractableComponent>();
            if (interactComponent != null)
            {
                interactComponent.Interact();
            }
        }
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
