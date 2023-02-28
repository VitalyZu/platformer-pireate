using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : Button
{
    [SerializeField] GameObject _normal;
    [SerializeField] GameObject _selected;
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);
        _normal.SetActive(state != SelectionState.Pressed);
        _selected.SetActive(state == SelectionState.Pressed);
    }
}
