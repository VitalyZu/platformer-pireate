using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraShakeEffect : MonoBehaviour
{
    [SerializeField] private float _animationTime = 0.3f;
    [SerializeField] private float _frequency;

    private CinemachineBasicMultiChannelPerlin _noise;
    private Coroutine _coroutine;

    private void Awake()
    {
        var camera = GetComponent<CinemachineVirtualCamera>();
        _noise = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void Shake()
    {
        if (_coroutine != null) StopAnimation();
        _coroutine = StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        _noise.m_FrequencyGain = _frequency;
        yield return new WaitForSeconds(_animationTime);
        StopAnimation();
    }

    private void StopAnimation()
    {
        _noise.m_FrequencyGain = 0f;
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}
