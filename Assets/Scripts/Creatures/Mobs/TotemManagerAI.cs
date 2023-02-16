using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemManagerAI : MonoBehaviour
{
    [SerializeField] TotemAI[] _totems;
    [SerializeField] private Cooldown _rangeCooldown;

    private int _index;

    private void Update()
    {
        if (_rangeCooldown.IsReady)
        {
            if (_totems[_index] != null)
            {
                _totems[_index]?.MakeAttack();
                _index = (int)Mathf.Repeat(_index + 1, _totems.Length);
                _rangeCooldown.Reset();
            }
            else
            {
                _index = (int)Mathf.Repeat(_index + 1, _totems.Length);
            }
            
        }   
    }
}
