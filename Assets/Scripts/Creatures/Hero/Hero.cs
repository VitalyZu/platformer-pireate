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
    [SerializeField] private Cooldown _throwCooldown;

    private static readonly int throwKey = Animator.StringToHash("throw");

    private Collider2D[] _interactResult = new Collider2D[1];    
    private SpriteRenderer _sprite;
    private bool _isArmed;      
    private bool _allowDoubleJump;
    private bool _wasDoubleJump = false;
    private int _coinsAmount = 0;
    private int _coinsValue = 0;
    private int _swordsValue = 0;
    private CinemachineFramingTransposer camBody;
    private GameSession _gameSession;
    private HealthComponent _healthComponent;
    private PlaySoundsComponent _soundsComponent;

    private int CoinsCount => _gameSession.Data.Inventory.Count("Coin");
    private int SwordCount => _gameSession.Data.Inventory.Count("Sword");
    private int HealthPotionCount => _gameSession.Data.Inventory.Count("Health_potion");



    override protected void Awake()
    {
        base.Awake();

        //Debug.Log(gameObject.layer);
        //Debug.Log(_groundMask.value);
        //Debug.Log(1 << gameObject.layer);
        //Debug.Log(_groundMask | (1 << gameObject.layer));
        camBody = _cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>(); 
        _sprite = GetComponent<SpriteRenderer>();
        _soundsComponent = GetComponent<PlaySoundsComponent>();
        
    }

    private void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();
        _healthComponent = GetComponent<HealthComponent>();

        _healthComponent.SetHealth(_gameSession.Data.HP);
        UpdateHeroWeapon();
        _swordsValue = SwordCount;
        if (_swordsValue != 0) _swordsValue++;
        _gameSession.Data.Inventory.OnChange += OnInventoryChange;
    }

    private void OnDestroy()
    {
        _gameSession.Data.Inventory.OnChange -= OnInventoryChange;
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
            DoJumpVFX();

            return velocity;
        }

        if (!IsGrounded && IsFalling)
        {
            velocity = _jumpSpeed;
            _wasDoubleJump = true;
            IsFalling = false;
            DoJumpVFX();

            return velocity;
            
        }

        return base.CalculateJumpVelocity(velocity);
    }

    public void OnInventoryChange(string id, int value)
    {
        if (id == "Sword") UpdateHeroWeapon();
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

        if (CoinsCount > 0)
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
        UpdateSwords(-1);
        _soundsComponent.Play("Range");
        //_gameSession.Data.IsArmed = false;
        //UpdateHeroWeapon();

    }
    public void Throw() 
    {
        if (_throwCooldown.IsReady && SwordCount > 1)
        {
            Animator.SetTrigger(throwKey);
            _throwCooldown.Reset();
        }
    }

    public void AddInInventory(string id, int value)
    {
        _gameSession.Data.Inventory.AddItem(id, value);
    }
    public override void Attack()
    {
        if (SwordCount <= 0) return;
        base.Attack();    
    }

    private void UpdateHeroWeapon()
    {
        if (SwordCount > 0)
        {
            Animator.runtimeAnimatorController = _armedAnimatorController;
        }
        else 
        {
            Animator.runtimeAnimatorController = _unarmedAnimatorController;
        }
    }

    private void UpdateSwords(int value)
    {
        _swordsValue += value;
        //_gameSession.Data.Swords = _swordsValue;
    }

    public void setCoins(int coins)
    {
        _gameSession.Data.Inventory.Count("Coins");
        //_gameSession.Data.Coins++;
        //_gameSession.Data.CoinsAmount += coins;
        //Debug.Log($"Coins: {_gameSession.Data.Coins}");
    }

    public void Heal()
    {
        if (HealthPotionCount > 0)
        {
            _gameSession.Data.Inventory.RemoveItem("Health_potion", 1);
            _healthComponent.DealHealth(1);
        }
    }
}
