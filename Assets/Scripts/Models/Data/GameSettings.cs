using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameSettings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private FloatPersistentProperty Music;
    [SerializeField] private FloatPersistentProperty Sfx;

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
        Music = new FloatPersistentProperty(1, SoundSetting.Music.ToString());
        Sfx = new FloatPersistentProperty(1, SoundSetting.SFX.ToString());

    }

    private void OnValidate()
    {
        Music.Validate();
        Sfx.Validate();
    }

}

public enum SoundSetting { 
    Music,
    SFX
}