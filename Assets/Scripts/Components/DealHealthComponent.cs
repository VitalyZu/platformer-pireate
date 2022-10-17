using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealHealthComponent : MonoBehaviour
{
    [SerializeField] private int _dealHealth;

    public void ApplyHealth(GameObject target)
    {
        HealthComponent healthComponent = target.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.DealHealth(_dealHealth);
        }
    }
}
