using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpawnComponent))]
public class CheckPointComponent : MonoBehaviour
{
    [SerializeField] private string _id;
    [SerializeField] UnityEvent _setChecked;
    [SerializeField] UnityEvent _setUnchecked;

    private GameSession _session;
    private SpawnComponent _heroSpawner;

    private void Start() //На Awake GameSession будет еще не проинициализирована
    {
        _heroSpawner = GetComponent<SpawnComponent>();
        _session = FindObjectOfType<GameSession>();
        if (_session.IsChecked(_id)) 
            _setChecked?.Invoke();
        else 
            _setUnchecked?.Invoke();
    }

    public void Check()
    {
        _session.SetChecked(_id);
    }

    public void SpawnHero()
    {
        _heroSpawner.Spawn();
    }
}
