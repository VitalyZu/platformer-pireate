using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _barrelMask;
    private Collider2D _collider;

    public bool isTouchingLayer;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision) 
    {
        isTouchingLayer = _collider.IsTouchingLayers(_groundMask) || _collider.IsTouchingLayers(_barrelMask);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouchingLayer = _collider.IsTouchingLayers(_groundMask) || _collider.IsTouchingLayers(_barrelMask);
    }
}
