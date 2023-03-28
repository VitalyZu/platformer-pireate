using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private string _startName;
    [SerializeField] private int _frameRate;
    [SerializeField] private UnityEvent _onComplete;
    [SerializeField] private bool _isParticle = false;
    [SerializeField] private AnimationClip[] _animationsClip;

    private SpriteRenderer _spriteRenderer;
    private float _secondsPerFrame;
    private int _currentSpriteIndex;
    private float _nextFrameTime;
    private int? _clipIndex;

    private bool _isPlaying = true;

    public string animationName { get; private set; }

    private void Awake()
    {
        enabled = _isParticle;
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _secondsPerFrame = 1f / _frameRate;
        _nextFrameTime = Time.time + _secondsPerFrame;

        SetAnimationByName(_startName);        
    }

    private void Update()
    {
        if (_isPlaying)
        {
            SetClip((int)_clipIndex);
        }
        
    }

    private void SetClip(int index)
    {
        animationName = _animationsClip[index].name;
        if (_nextFrameTime > Time.time) return;
        if (_currentSpriteIndex >= _animationsClip[index].sprites.Length)
        {
            if (_animationsClip[index].allowNext)
            {
                if (index >= _animationsClip.Length - 1) {
                    _clipIndex = 0;
                }
                else {
                    _clipIndex++;
                }

                _currentSpriteIndex = 0;

                _animationsClip[index].onAnimationComplete?.Invoke();

                return;
            }
            if (_animationsClip[index].loop)
            {
                _currentSpriteIndex = 0;
            }
            else
            {
                enabled = _isPlaying = false;

                _animationsClip[index].onAnimationComplete?.Invoke();

                _onComplete?.Invoke();
                
                return;
            }
        }
        _spriteRenderer.sprite = _animationsClip[index].sprites[_currentSpriteIndex];
        _currentSpriteIndex++;
        _nextFrameTime += _secondsPerFrame;
    }

    public void SetAnimationByName(string name)
    {
        _currentSpriteIndex = 0;
        _nextFrameTime = Time.time + _secondsPerFrame;
        for (int i = 0; i < _animationsClip.Length; i++)
        {
            if (_animationsClip[i].name == name)
            {
                _clipIndex = i;
                enabled = _isPlaying = true;
            }
        }
        if (_clipIndex == null)
        {
            if (_animationsClip.Length > 0) _clipIndex = 0;
            else _isPlaying = false;
        }
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }
    private void OnBecameVisible()
    {
        enabled = _isPlaying;
    }

    [Serializable]
    public class AnimationClip
    {
        public string name;
        public bool loop;
        public Sprite[] sprites;
        public bool allowNext;
        public UnityEvent onAnimationComplete;
    }
}
