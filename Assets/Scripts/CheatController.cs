using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheatController : MonoBehaviour
{

    private void Awake()
    {
        Keyboard.current.onTextInput += onKeyInput;
    }

    private void onKeyInput(char c)
    { 
    }
}
