using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameSettings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private FloatPersistentProperty _music;
    [SerializeField] private FloatPersistentProperty _sfx;

    public FloatPersistentProperty Music => _music;
    public FloatPersistentProperty Sfx => _sfx;

    //Singleton
    private static GameSettings _instance;
    public static GameSettings I => _instance == null ? LoadGameSettings() : _instance;
    private static GameSettings LoadGameSettings()
    {
        return _instance = Resources.Load<GameSettings>("GameSettings");
    }

    private void OnEnable() 
    {
        //Обращаться нужно в OnEnable, т к в констукторе не даст обратиться к PlayerPrefs
        _music = new FloatPersistentProperty(1, SoundSetting.Music.ToString());
        _sfx = new FloatPersistentProperty(1, SoundSetting.SFX.ToString());

    }

    private void OnValidate()
    {
        _music.Validate();
        _sfx.Validate();
    }

}

public enum SoundSetting { 
    Music,
    SFX
}