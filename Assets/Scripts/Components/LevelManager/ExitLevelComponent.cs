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
        //session.initPlayerData = (PlayerData)session.Data.Clone();
        session.Save();
        SceneManager.LoadScene(_sceneName);
    }
}
