using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevelComponent : MonoBehaviour
{
    public void RestartLevel()
    {
        GameSession session = FindObjectOfType<GameSession>();
        session.Data = session.initPlayerData;
        session.initPlayerData = (PlayerData)session.Data.Clone();

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
