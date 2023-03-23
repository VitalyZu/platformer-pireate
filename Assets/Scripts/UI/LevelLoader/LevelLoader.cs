using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _transitionTime;

    private static readonly int enabledKey = Animator.StringToHash("enabled");

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void AfterSceneLoad()
    {
        Debug.Log("SCENE LOADER");
        InitLoader();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private static void InitLoader()
    {
        SceneManager.LoadScene("LevelLoader", LoadSceneMode.Additive);
    }

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(StartAnimation(sceneName));
    }

    private IEnumerator StartAnimation(string sceneName)
    {
        _animator.SetBool(enabledKey, true);
        Debug.Log("Set enabled trigger");
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadScene(sceneName);
        _animator.SetBool(enabledKey, false);
    }
}
