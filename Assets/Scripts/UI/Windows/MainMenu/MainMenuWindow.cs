using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuWindow : AnimatedWindow
{
    private Action _closeAction;

    public void OnShowSettings()
    {
        var window = Resources.Load("UI/SettingsWindow");
        var canvas = FindObjectOfType<Canvas>();
        Instantiate(window, canvas.transform);
    }

    public void OnLanguagse()
    {
        var window = Resources.Load("UI/Localization");
        var canvas = FindObjectOfType<Canvas>();
        Instantiate(window, canvas.transform);
    }

    public void OnStartGame()
    {
        _closeAction = () =>
        {
            LevelLoader loader = FindObjectOfType<LevelLoader>();
            loader.LoadLevel("SampleScene");
        };
        Close();
    }

    public void OnExit()
    {
        _closeAction = () =>
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        };
        Close();
    }

    public override void OnCloseAnimationComplete()
    {
        base.OnCloseAnimationComplete();
        Debug.Log("CloseAnim Complete");
        _closeAction?.Invoke();
    }
}
