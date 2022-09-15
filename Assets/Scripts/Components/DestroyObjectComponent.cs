using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectComponent : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    public void DestroySelf()
    {
        if (_gameObject != null)
        {
            Destroy(_gameObject);
        }

        Destroy(gameObject);
    }
}
