using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using Cinemachine;
using System;

public class Hero : Creature, ICanAddInInventory
{
    //[SerializeField] private CinemachineVirtualCamera _cinemachineCamera;
    private CinemachineVirtualCamera _cinemachineCamera;
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
    [SerializeField] private SpawnComponent _throwSpawner;

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

    private const string SwordId = "Sword";
    private int CoinsCount => _gameSession.Data.Inventory.Count("Coin");
    private int SwordCount => _gameSession.Data.Inventory.Count(SwordId);
    private int HealthPotionCount => _gameSession.Data.Inventory.Count("Health_potion");

    private string SelectedItemId => _gameSession.QuickInventory.SelectedItem.Id;

    private bool CanThrow
    {
        get {
            if (SelectedItemId == SwordId)
            {
                return SwordCount > 1;
            }

            var def = DefsFacade.I.Items.Get(SelectedItemId);
            return def.HasTag(ItemTag.Throwable);
        }
    }


    override protected void Awake()
    {
        base.Awake();

        //Debug.Log(gameObject.layer);
        //Debug.Log(_groundMask.value);
        //Debug.Log(1 << gameObject.layer);
        //Debug.Log(_groundMask | (1 << gameObject.layer));
        
        _sprite = GetComponent<SpriteRenderer>();
        _soundsComponent = GetComponent<PlaySoundsComponent>();
        
    }

    private void Start()
    {
        _cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _gameSession = FindObjectOfType<GameSession>();
        _healthComponent = GetComponent<HealthComponent>();

        _healthComponent.SetHealth(_gameSession.Data.HP.Value);
        UpdateHeroWeapon();
        _swordsValue = SwordCount;
        if (_swordsValue != 0) _swordsValue++;
        _gameSession.Data.Inventory.OnChange += OnInventoryChange;
        camBody = _cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
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
        if (id == SwordId) UpdateHeroWeapon();
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
        _gameSession.Data.HP.Value = currentHealth;
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
        //_particles.Spawn("Throw");
        
        var throwableId = _gameSession.QuickInventory.SelectedItem.Id;
        var throwableDef = DefsFacade.I.Throwable.Get(throwableId);
        _throwSpawner.SetPrefab(throwableDef.Projectile);
        _throwSpawner.Spawn();
        UpdateSwords(-1);
        _soundsComponent.Play("Range");
        _gameSession.Data.Inventory.RemoveItem(throwableId, 1);
        //_gameSession.Data.IsArmed = false;
        //UpdateHeroWeapon();

    }
    public void Throw() 
    {
        if (_throwCooldown.IsReady && CanThrow)
        {
            Animator.SetTrigger(throwKey);
            //_gameSession.Data.Inventory.RemoveItem("Sword", 1);
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
        if (HealthPotionCount > 0 && _gameSession.Data.HP.Value < DefsFacade.I.Player.MaxHealth)
        {
            _gameSession.Data.Inventory.RemoveItem("Health_potion", 1);
            _healthComponent.DealHealth(1);
        }
    }

    public void NextItem()
    {
        _gameSession.QuickInventory.SetNextItem();
    }
}
