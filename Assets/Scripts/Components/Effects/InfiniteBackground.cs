using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _container;

    private Bounds _containerBounds;
    private Bounds _allBounds;

    private Vector3 _boundsToTransformDelta;
    private Vector3 _containerDelta;

    private void Start()
    {
        var sprites = _container.GetComponentsInChildren<SpriteRenderer>();
        _containerBounds = sprites[0].bounds;
        foreach (var sprite in sprites)
        {
            _containerBounds.Encapsulate(sprite.bounds);
        }

        _allBounds = _containerBounds;

        _boundsToTransformDelta = transform.position - _allBounds.center;
        _containerDelta = _container.position - _allBounds.center;
    }

    private void OnDrawGizmos()
    {
        GizmoUtils.DrawBounds(_allBounds, Color.magenta);
    }
}
