using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    public PlayerData Data { get; private set; }

    private void Awake()
    {
        if (IsSessionExist())
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private bool IsSessionExist()
    {
        GameSession[] sessions = FindObjectsOfType<GameSession>();
        foreach (GameSession session in sessions)
        {
            if (session != this)
            {
                return true;
            }
        }
        return false;
    }
}
