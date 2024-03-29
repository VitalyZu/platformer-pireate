﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Cooldown
{
    [SerializeField] public float _value;
    private float _timeUp;

    public void Reset() 
    {
        _timeUp = Time.time + _value;
    }

    public bool IsReady => _timeUp <= Time.time;
}
