using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class CheatController : MonoBehaviour
{
    [SerializeField] Cheat[] _cheats;
    [SerializeField] float _inputTimeToLive;
    private float _inputTime;
    private string _currentString;

    private void Awake()
    {
        Keyboard.current.onTextInput += OnKeyInput;
    }

    private void OnDestroy()
    {
        Keyboard.current.onTextInput -= OnKeyInput;
    }

    private void Update()
    {
        if (_inputTime < 0)
        {
            _currentString = string.Empty;
        }
        else
        {
            _inputTime -= Time.deltaTime;
        }
    }

    private void OnKeyInput(char c)
    {
        _currentString += c;
        _inputTime = _inputTimeToLive;
        FindCheat();
    }

    private void FindCheat()
    {
        foreach (var item in _cheats)
        {
            if (_currentString.Contains(item.Name))
            {
                item.Action?.Invoke();
                _currentString = string.Empty;
            }
        }
    
    }
}

[Serializable]
public class Cheat
{
    public string Name;
    public UnityEvent Action;
}
