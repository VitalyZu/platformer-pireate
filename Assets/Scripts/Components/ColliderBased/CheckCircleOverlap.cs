using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.Events;
using System.Linq;

public class CheckCircleOverlap : MonoBehaviour
{
    [SerializeField] float _radius = 1f;
    [SerializeField] OnOverlapEvent _onOverlap;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private string[] _tags;
    private Collider2D[] _interactResult = new Collider2D[10];

    public void Check()
    {
        int hit = Physics2D.OverlapCircleNonAlloc(transform.position, 
            _radius, 
            _interactResult,
            _mask);

        for (int i = 0; i < hit; i++)
        {
            var isCompare = _tags.Any(x=> _interactResult[i].gameObject.CompareTag(x));
            
            if(isCompare) _onOverlap?.Invoke(_interactResult[i].gameObject);
        }
    }

    [Serializable]
    public class OnOverlapEvent : UnityEvent<GameObject>
    { 
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = HandlesUtils.TransparentRed;
        Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
    }
#endif
}
