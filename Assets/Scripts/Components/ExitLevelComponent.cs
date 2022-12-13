using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevelComponent : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    public void Exit()
    {
        GameSession session = FindObjectOfType<GameSession>();
        session.initPlayerData = (PlayerData)session.Data.Clone();

        SceneManager.LoadScene(_sceneName);
    }
}
