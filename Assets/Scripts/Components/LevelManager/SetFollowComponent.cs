using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class SetFollowComponent : MonoBehaviour
{
    private void Start()
    {
        var vCamera = GetComponent<CinemachineVirtualCamera>();
        vCamera.Follow = FindObjectOfType<Hero>().transform;
    }
}
