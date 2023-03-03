﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsWidget : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _value;

    private FloatPersistentProperty _model;

    private void Start()
    {
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void SetModel(FloatPersistentProperty model)
    {
        _model = model;
        model.onChanged += OnValueChanged;
        OnValueChanged(model.Value, model.Value);
    }

    private void OnValueChanged(float newValue, float oldValue)
    {
        var textValue = Mathf.Round(newValue * 100);
        _value.text = textValue.ToString();
        _slider.normalizedValue = newValue;
    }

    private void OnSliderValueChanged(float value)
    {
        _model.Value = value;
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        _model.onChanged -= OnValueChanged;
    }

}