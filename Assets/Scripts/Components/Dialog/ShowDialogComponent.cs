using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class ShowDialogComponent : MonoBehaviour
{
    [SerializeField] private Mode _mode;
    [SerializeField] private DialogData _bound;
    [SerializeField] private DialogDef _external;
    [SerializeField] private UnityEvent _onComplete;

    private DialogBoxController _dialogBox;

    public void Show()
    {
        if (_dialogBox == null) _dialogBox = FindObjectOfType<DialogBoxController>();
        var data = Data;
        _dialogBox.ShowDialog(data, _onComplete);
    }

    public void Show(DialogDef def)
    {
        _external = def;
        Show();
    }

    private DialogData Data 
    {
        get {
            switch (_mode)
            {
                case Mode.Bound:
                    return _bound;
                case Mode.External:
                    return _external.Data;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum Mode
    {
        Bound,
        External
    }
}
