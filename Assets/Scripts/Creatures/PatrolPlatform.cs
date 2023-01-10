using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPlatform : Patrol
{
    [SerializeField] private Transform _checkPoint;
    [SerializeField] private LayerMask _layer;

    private Creature _creature;
    private Vector2 _direction = Vector2.left;

    private void Awake()
    {
        _creature = GetComponent<Creature>();
    }
    public override IEnumerator DoPatrol()
    {
        while (enabled)
        {
            RaycastHit2D rayCast = Physics2D.Raycast(_checkPoint.position, Vector2.down, 1f, _layer);

            if (rayCast.collider == null)
            {
                _direction *= -1;
            }

            _creature.SetDirection(_direction);

            yield return null;
        }
    }
}
