using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportComponent : MonoBehaviour
{
    [SerializeField] Transform _moveTo;

    public void Teleport(GameObject target)
    {
        target.transform.position = _moveTo.position;
    }
}
