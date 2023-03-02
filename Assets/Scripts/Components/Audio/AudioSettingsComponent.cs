using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class AudioSettingsComponent : MonoBehaviour
{
    [SerializeField] private SoundSetting _mode;
    private FloatPersistentProperty _model;
    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _model = FindProperty();
        _model.onChanged += OnSoundSettingsChanged;
        OnSoundSettingsChanged(_model.Value, _model.Value);
    }

    private void OnSoundSettingsChanged(float newValue, float oldValue)
    {
        _source.volume = newValue;
    }

    private void OnDestroy()
    {
        _model.onChanged -= OnSoundSettingsChanged;
    }

    private FloatPersistentProperty FindProperty()
    {
        switch (_mode)
        {
            case SoundSetting.Music:
                return GameSettings.I.Music;
            case SoundSetting.SFX:
                return GameSettings.I.Sfx;
        }
        throw new ArgumentException("Undefined mode");
    }
}
