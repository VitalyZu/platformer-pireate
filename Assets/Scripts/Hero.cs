using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector2 _direction;

    private void Update() 
    {
        if (_direction != Vector2.zero)
        {
            Vector3 delta = _direction * _speed * Time.deltaTime;
            transform.position = transform.position + delta;
        }
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void SaySomething()
    {
        Debug.Log("Say something");
    }
}
