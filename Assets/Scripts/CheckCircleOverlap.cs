using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CheckCircleOverlap : MonoBehaviour
{
    [SerializeField] float _radius = 1f;
    private Collider2D[] _interactResult = new Collider2D[10];

    public GameObject[] CheckObjectsInRange()
    {
        int hit = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _interactResult);

        List<GameObject> overlap = new List<GameObject>(5);

        for (int i = 0; i < hit; i++)
        {
            overlap.Add(_interactResult[i].gameObject);
        }

        return overlap.ToArray();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = HandlesUtils.TransparentRed;
        Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
    }
#endif
}
