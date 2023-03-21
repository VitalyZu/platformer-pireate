using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatWidget : MonoBehaviour, IItemRenderer<StatDef>
{
    [SerializeField] private Text _name;
    [SerializeField] private Image _icon;
    [SerializeField] private Text _currentValue;
    [SerializeField] private Text _increaseValue;
    [SerializeField] private ProgressBarWidget _progress;
    [SerializeField] private GameObject _selector;
    
    public void SetData(StatDef data, int index)
    {
        throw new System.NotImplementedException();
    }
}
