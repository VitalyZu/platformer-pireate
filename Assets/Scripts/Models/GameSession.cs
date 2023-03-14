using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameSession : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private string _defaultCheckPoint;
    //public PlayerData initPlayerData;
    private PlayerData _save;
    //public PlayerData Data => _playerData;
    private List<string> _checkPoints = new List<string>();
    private readonly CompositeDisposable _trash = new CompositeDisposable();
    public PlayerData Data 
    {
        get { return _playerData; }
        set { _playerData = value; }
    }

    public QuickInventoryModel QuickInventory { get; private set; }

    private void Awake()
    {
        var existSession = GetExistSession();
        if (existSession != null)
        {
            existSession.StartSession(_defaultCheckPoint);
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            //initPlayerData = (PlayerData)_playerData.Clone();
            Save();
            InitModels();
            StartSession(_defaultCheckPoint);
        }
        
    }
    private void StartSession(string checkPoint)
    {
        SetChecked(checkPoint);
        LoadHUD();
        SpawnHero();
    }

    private void SpawnHero()
    {
        var checkPoints = FindObjectsOfType<CheckPointComponent>();
        var lastCheckPoint = _checkPoints.Last();
        foreach (var item in checkPoints)
        {
            if (item.Id == lastCheckPoint)
            {
                item.SpawnHero();
                break;
            }
        }
    }

    private void InitModels()
    { 
        QuickInventory = new QuickInventoryModel(Data);
        _trash.Retain(QuickInventory);
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

    private GameSession GetExistSession()
    {
        GameSession[] sessions = FindObjectsOfType<GameSession>();
        foreach (GameSession session in sessions)
        {
            if (session != this)
            {
                return session;
            }
        }
        return null;
    }
    //Check Points
    public bool IsChecked(string id)
    {
        return _checkPoints.Contains(id);
    }

    public void SetChecked(string id)
    {
        if (!_checkPoints.Contains(id))
        {
            _checkPoints.Add(id);
            Save();
        }
    }


    private void OnDestroy()
    {
        _trash.Dispose();
    }
}
