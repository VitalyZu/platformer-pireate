using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseProjetile
{
    protected override void Start()
    {
        base.Start();
        var force = new Vector2(_speed * Direction, 0);
        Rigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}
