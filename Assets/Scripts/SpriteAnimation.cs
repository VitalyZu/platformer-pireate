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
    [SerializeField] private AnimationClip[] _animationsClip;

    private SpriteRenderer _spriteRenderer;
    private float _secondsPerFrame;
    private int _currentSpriteIndex;
    private float _nextFrameTime;
    private int? _clipIndex;

    private bool _isPlaying = true;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _secondsPerFrame = 1f / _frameRate;
        _nextFrameTime = Time.time + _secondsPerFrame;
        
        for (int i = 0; i < _animationsClip.Length; i++)
        {
            if (_animationsClip[i].name == _startName)
            {
                _clipIndex = i;
            }
        }
        if (_clipIndex == null) {
            if (_animationsClip.Length > 0) _clipIndex = 0;
            else _isPlaying = false;
        }
        
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
                return;
            }
            if (_animationsClip[index].loop)
            {
                _currentSpriteIndex = 0;
            }
            else
            {
                _isPlaying = false;
                _onComplete?.Invoke();
                return;
            }
        }
        _spriteRenderer.sprite = _animationsClip[index].sprites[_currentSpriteIndex];
        _currentSpriteIndex++;
        _nextFrameTime += _secondsPerFrame;
    }

    [Serializable]
    public class AnimationClip
    {
        public string name;
        public bool loop;
        public Sprite[] sprites;
        public bool allowNext;
    }
}
