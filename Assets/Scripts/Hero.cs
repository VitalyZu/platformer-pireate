using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using Cinemachine;
using System;

public class Hero : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineCamera;
    [Space][Header("Particles")]
    [SerializeField] private SpawnComponent _spawnStepsComponent;
    [SerializeField] private SpawnComponent _spawnJumpComponent;
    [SerializeField] private SpawnComponent _spawnDownComponent;
    [SerializeField] private SpawnComponent _spawnAttackComponent;
    [SerializeField] private ParticleSystem _hitParticle;
    [Space]
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _damageJumpSpeed;
    [SerializeField] private float _slamDownVelocity;
    [SerializeField] private LayerMask _groundMask;

    [Space]
    [SerializeField] CheckCircleOverlap _attackOverlap;

    [Space]
    [SerializeField] private AnimatorController _armedAnimatorController;
    [SerializeField] private AnimatorController _unarmedAnimatorController;

    [Space]
    [SerializeField] private float _interactRadius;
    [SerializeField] private LayerMask _interactMask;
    private Collider2D[] _interactResult = new Collider2D[1];

    [SerializeField] private LayerCheck _layerCheck;

    private static readonly int isGroundKey = Animator.StringToHash("isGround");
    private static readonly int verticalVelocityKey = Animator.StringToHash("verticalVelocity");
    private static readonly int isRunningKey = Animator.StringToHash("isRunning");
    private static readonly int hitKey = Animator.StringToHash("hit");
    private static readonly int attackKey = Animator.StringToHash("attack");

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private Animator _animator;
    private Vector2 _direction;

    private bool _isArmed;
    private bool _isJumping;
    private bool _isGrounded;
    private bool _allowDoubleJump;
    private bool _allowFallingJump;
    private bool _wasDoubleJump = false;

    private int _coinsAmount = 0;
    private int _coinsValue = 0;

    private CinemachineFramingTransposer camBody;

    public bool isHit { get; private set; } 

    private void Awake()
    {
        //Debug.Log(gameObject.layer);
        //Debug.Log(_groundMask.value);
        //Debug.Log(1 << gameObject.layer);
        //Debug.Log(_groundMask | (1 << gameObject.layer));
        camBody = _cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
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
        if (_direction.x != 0)
        {
            camBody.m_DeadZoneWidth = camBody.m_DeadZoneWidth * .9f;
        }
        else
        {
            camBody.m_DeadZoneWidth = 0.8f;
        }

        float xVelocity = _direction.x * _speed;
        float yVelocity = CalculateYVelocity();

        _rigidbody.velocity = new Vector2(xVelocity, yVelocity);
 
        _animator.SetBool(isGroundKey, _isGrounded);

        float velocityForAnimator = _rigidbody.velocity.y;

        if (_rigidbody.velocity.y > -0.9 && !_isJumping) velocityForAnimator = 0;

        _animator.SetFloat(verticalVelocityKey, velocityForAnimator);
        
        _animator.SetBool(isRunningKey, _direction.x != 0);

        SetSpriteDirection();
    }

    private float CalculateYVelocity()
    {
        float yVelocity = _rigidbody.velocity.y;

        bool isJumping = _direction.y > 0;

        if (_isGrounded)
        {
            _isJumping = false;
            _allowDoubleJump = true;
            _allowFallingJump = true;
        }

        if (isJumping)
        {
            _isJumping = true;
            yVelocity = CalculateJumpVelocity(yVelocity);
        }
        else if (_rigidbody.velocity.y > 0 && _isJumping)
        {
            yVelocity *= .5f;
        }

        return yVelocity;
    }

    private float CalculateJumpVelocity(float velocity)
    {
        bool isFalling = _rigidbody.velocity.y <= .001f;

        if (!isFalling) return velocity;
     

        if (_isGrounded || _allowFallingJump)
        {
            _wasDoubleJump = false;
            velocity += _jumpSpeed;
            if(_allowFallingJump) velocity = _jumpSpeed;
            _allowFallingJump = false;
            _spawnJumpComponent.Spawn();
        }
        else if (_allowDoubleJump) 
        {
            _wasDoubleJump = true;
            velocity = _jumpSpeed;
            _allowDoubleJump = false;
            _spawnJumpComponent.Spawn();
        }

        return velocity;
    }

    private void SetSpriteDirection()
    {
        if (_direction.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (_direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.IsInLayer(_groundMask))
        {
            var contact = collision.contacts[0];
            if(contact.relativeVelocity.y >= _slamDownVelocity)
            {
                _spawnDownComponent.Spawn();
            }
            else if (_wasDoubleJump == true)
            {
                _spawnDownComponent.Spawn();
            }
        }
    }

    private void SpawnCoins()
    {
        int coinsToDispose = Mathf.Min(_coinsValue, 5);
        _coinsValue -= coinsToDispose;

        ParticleSystem.Burst burst = _hitParticle.emission.GetBurst(0);

        burst.count = coinsToDispose;

        _hitParticle.emission.SetBurst(0, burst);

        _hitParticle.gameObject.SetActive(true);
        _hitParticle.Play();
    }

    public void GetDamage()
    {
        _isJumping = false;
        isHit = true;
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageJumpSpeed);
        _animator.SetTrigger(hitKey);

        if (_coinsAmount > 0)
        {
            SpawnCoins();
        }        
    }
    public void EndHit()
    {
        isHit = false;
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

    public void Attack()
    {
        if (!_isArmed) return;
        _animator.SetTrigger(attackKey);
        
    }

    //Event trigger
    public void MakeAttack()
    {
        GameObject[] gos = _attackOverlap.CheckObjectsInRange();
        foreach (var item in gos)
        {
            HealthComponent objHealthCom = item.GetComponent<HealthComponent>();
            if (objHealthCom != null && item.CompareTag("Enemy"))
            {
                objHealthCom.DealHealth(-_damage);
            }
        }
    }

    public void ArmHero()
    {
        _isArmed = true;

        _animator.runtimeAnimatorController = _armedAnimatorController;
    }

    public bool IsGround() 
    {
        Debug.Log(_layerCheck.isTouchingLayer);
        return _layerCheck.isTouchingLayer;
    }

    public void setCoins(int coins)
    {
        _coinsValue++;
        _coinsAmount += coins;
        Debug.Log($"Coins: {_coinsValue} ({ _coinsAmount} $)");
    }

    public void SpawnStepsDust()
    {
        _spawnStepsComponent.Spawn();
    }

    public void SpawnJumpDust()
    {
        _spawnJumpComponent.Spawn();
    }

    public void SpawnAttackDust()
    {
        _spawnAttackComponent.Spawn();
    }
}
