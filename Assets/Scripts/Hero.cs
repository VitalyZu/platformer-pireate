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
            Vector2 delta = _direction * _speed * Time.deltaTime;
            float posX = transform.position.x + delta.x;
            float posY = transform.position.y + delta.y;
            transform.position = new Vector3(posX, posY, transform.position.z);
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
