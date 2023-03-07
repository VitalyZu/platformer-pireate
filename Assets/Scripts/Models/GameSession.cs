using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    //public PlayerData initPlayerData;
    private PlayerData _save;
    //public PlayerData Data => _playerData;
    public PlayerData Data 
    {
        get { return _playerData; }
        set { _playerData = value; }
    }

    public QuickInventoryModel QuickInventory { get; private set; }

    private void Awake()
    {       
        if (IsSessionExist())
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            //initPlayerData = (PlayerData)_playerData.Clone();
        }
        Save();
        InitModels();
        LoadHUD();
    }

    private void InitModels()
    { 
        QuickInventory = new QuickInventoryModel(Data);
    }

    private void LoadHUD()
    {
        SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
    }

    public void Save()
    {
        _save = _playerData.Clone();
    }

    public void LoadLastSave()
    {
        _playerData = _save.Clone();
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
