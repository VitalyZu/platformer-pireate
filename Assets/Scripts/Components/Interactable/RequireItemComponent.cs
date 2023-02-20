using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RequireItemComponent : MonoBehaviour
{
    [SerializeField] InventoryItemData[] _required; 
    [SerializeField] private bool _removeAfterUse;

    [SerializeField] private UnityEvent _onSucces;
    [SerializeField] private UnityEvent _onFail;

    public void Check()
    {
        var session = FindObjectOfType<GameSession>();

        var isMeet = true;
        foreach (var item in _required)
        {
            var count = session.Data.Inventory.Count(item.Id);
            if (count < item.Value) isMeet = false;
        }

        
        if (isMeet)
        {
            if (_removeAfterUse)
            {
                foreach (var item in _required)
                {
                    session.Data.Inventory.RemoveItem(item.Id, item.Value);
                }
            }
             
            _onSucces?.Invoke();
        }
        else
        {
            _onFail?.Invoke();
        }
    }
}
