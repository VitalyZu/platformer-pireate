using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DialogData
{
    [SerializeField] string[] _sentences;
    public string[] Sentences => _sentences;
}
