using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using Cinemachine;
using System;

public class Hero : Creature
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineCamera;
    [Space]   
    [SerializeField] private float _slamDownVelocity;
    [Space]
    [Header("Animator controllers")]
    [SerializeField] private AnimatorController _armedAnimatorController;
    [SerializeField] private AnimatorController _unarmedAnimatorController;
    [Space]
    [Header("Hero interact")]
    [SerializeField] private CheckCircleOverlap _interactionCheck;
    //[SerializeField] private float _interactRadius;
    //[SerializeField] private LayerMask _interactMask;
    [Space]
    [Header("Particles")]
    [SerializeField] private ParticleSystem _hitParticle;

    private static readonly int throwKey = Animator.StringToHash("throw");

    private Collider2D[] _interactResult = new Collider2D[1];    
    private SpriteRenderer _sprite;
    private bool _isArmed;      
    private bool _allowDoubleJump;
    private bool _wasDoubleJump = false;
    private int _coinsAmount = 0;
    private int _coinsValue = 0;
    private CinemachineFramingTransposer camBody;
    private GameSession _gameSession;

    

    override protected void Awake()
    {
        base.Awake();

        //Debug.Log(gameObject.layer);
        //Debug.Log(_groundMask.value);
        //Debug.Log(1 << gameObject.layer);
        //Debug.Log(_groundMask | (1 << gameObject.layer));
        camBody = _cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>(); 
        _sprite = GetComponent<SpriteRenderer>();
        
    }

    private void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();
        var healthComponent = GetComponent<HealthComponent>();

        healthComponent.SetHealth(_gameSession.Data.HP);
        UpdateHeroWeapon();
    }

    protected override void FixedUpdate()
    {
        if (Direction.x != 0)
        {
            camBody.m_DeadZoneWidth = camBody.m_DeadZoneWidth * .9f;
        }
        else
        {
            camBody.m_DeadZoneWidth = 0.8f;
        }

        base.FixedUpdate();
    }

    protected override float CalculateYVelocity()
    {
        if (IsGrounded)
        {
            _allowDoubleJump = true;
            _wasDoubleJump = false;
            IsFalling = true;
        }

        return base.CalculateYVelocity();
    }

    protected override float CalculateJumpVelocity(float velocity)
    {
        if (!IsGrounded && _allowDoubleJump)
        {
            velocity = _jumpSpeed;
            _allowDoubleJump = false;
            _wasDoubleJump = true;
            _particles.Spawn("Jump");
            

            return velocity;
        }

        if (!IsGrounded && IsFalling)
        {
            velocity = _jumpSpeed;
            _wasDoubleJump = true;
            IsFalling = false;
            _particles.Spawn("Jump");

            return velocity;
            
        }

        return base.CalculateJumpVelocity(velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.IsInLayer(_groundMask))
        {
            var contact = collision.contacts[0];
            if(contact.relativeVelocity.y >= _slamDownVelocity)
            {
                _particles.Spawn("SlamDown");
            }
            else if (_wasDoubleJump == true)
            {
                _wasDoubleJump = false;
                _particles.Spawn("SlamDown");
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

    public void OnHealthChanged(int currentHealth)
    {
        _gameSession.Data.HP = currentHealth;
    }

    public override void GetDamage()
    {
        base.GetDamage();

        if (_gameSession.Data.Coins > 0)
        {
            SpawnCoins();
        }        
    }

    public void Interact()
    {
        _interactionCheck.Check();
    }

    public void OnDoThrow()
    {
        _particles.Spawn("Throw");
    }
    public void Throw() 
    {
        Animator.SetTrigger(throwKey);
    }

    public override void Attack()
    {
        if (!_gameSession.Data.IsArmed) return;
        base.Attack();    
    }

    public void ArmHero()
    {
        _gameSession.Data.IsArmed = true;
        UpdateHeroWeapon();      
    }

    private void UpdateHeroWeapon()
    {
        if (_gameSession.Data.IsArmed)
        {
            Animator.runtimeAnimatorController = _armedAnimatorController;
        }
        else 
        {
            Animator.runtimeAnimatorController = _unarmedAnimatorController;
        }
    }

    public void setCoins(int coins)
    {
        _gameSession.Data.Coins++;
        _gameSession.Data.CoinsAmount += coins;
        Debug.Log($"Coins: {_gameSession.Data.Coins} ({_gameSession.Data.CoinsAmount} $)");
    }
}
