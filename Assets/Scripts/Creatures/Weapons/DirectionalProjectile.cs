using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalProjectile : BaseProjetile
{
    public void Launch(Vector2 direction)
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.AddForce(direction * _speed, ForceMode2D.Impulse);
        //Rigidbody.AddForce(Vector2.up, ForceMode2D.Impulse);
    }
}
