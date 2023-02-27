using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] protected float _jumpSpeed;
    [SerializeField] private float _speed;
    [SerializeField] private float _damageJumpSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private bool _invertScale;
    [Space]
    [Header("Layers check")]
    [SerializeField] protected LayerMask _groundMask;
    [SerializeField] private LayerCheck _layerCheck;
    [Space]
    [SerializeField] CheckCircleOverlap _attackOverlap;
    [SerializeField] protected SpawnListComponent _particles;

    

    protected Rigidbody2D Rigidbody;
    protected Vector2 Direction;
    protected bool IsGrounded;
    protected Animator Animator;
    protected PlaySoundsComponent Sounds;
    protected bool IsJumping;
    protected bool IsFalling;

    private static readonly int isGroundKey = Animator.StringToHash("isGround");
    private static readonly int verticalVelocityKey = Animator.StringToHash("verticalVelocity");
    private static readonly int isRunningKey = Animator.StringToHash("isRunning");
    private static readonly int hitKey = Animator.StringToHash("hit");
    private static readonly int attackKey = Animator.StringToHash("attack");

    public bool isHit { get; private set; }

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Sounds = GetComponent<PlaySoundsComponent>();
    }

    private void Update()
    {
        IsGrounded = _layerCheck.isTouchingLayer;
    }

    protected virtual void FixedUpdate()
    {
        float xVelocity = Direction.x * _speed;
        float yVelocity = CalculateYVelocity();

        Rigidbody.velocity = new Vector2(xVelocity, yVelocity);

        Animator.SetBool(isGroundKey, IsGrounded);

        float velocityForAnimator = Rigidbody.velocity.y;

        if (Rigidbody.velocity.y > -0.9 && !IsJumping) velocityForAnimator = 0;

        Animator.SetFloat(verticalVelocityKey, velocityForAnimator);

        Animator.SetBool(isRunningKey, Direction.x != 0);

        SetSpriteDirection(Direction);
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void SetSpriteDirection( Vector2 direction)
    {
        var multiplier = _invertScale ? -1 : 1;
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(multiplier, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1 * multiplier, 1, 1);
        }
    }

    protected virtual float CalculateYVelocity()
    {
        float yVelocity = Rigidbody.velocity.y;

        bool isJumping = Direction.y > 0;

        if (IsGrounded)
        {
            IsJumping = false;
        }

        if (isJumping)
        {
            IsJumping = true;
            bool isFalling = Rigidbody.velocity.y <= .001f;
            yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
        }
        else if (Rigidbody.velocity.y > 0 && IsJumping)
        {
            yVelocity *= .5f;
        }
        else if (Rigidbody.velocity.y < 0.001)
        {
            
        }

        return yVelocity;
    }

    protected virtual float CalculateJumpVelocity(float velocity)
    {
        
        if (IsGrounded)
        {
            velocity += _jumpSpeed;
            DoJumpVFX();
            IsFalling = false;
        }

        return velocity;
    }

    protected void DoJumpVFX()
    {
        _particles.Spawn("Jump");
        Sounds.Play("Jump");
    }

    public virtual void GetDamage()
    {
        IsJumping = false;
        isHit = true;
        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageJumpSpeed);
        Animator.SetTrigger(hitKey);
    }

    public void EndHit()
    {
        isHit = false;
    }

    public virtual void Attack()
    {
        Animator.SetTrigger(attackKey);
        Sounds.Play("Melee");
    }

    //Event trigger
    public void MakeAttack()
    {
        _attackOverlap.Check();
    }
}
