using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaySoundsComponent : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioData[] _sounds;

    public void Play(string id)
    {
        foreach (var data in _sounds)
        {
            if (data.Id != id) continue;
            
            _source.PlayOneShot(data.Clip);
            break;
            
        }
    }

    [Serializable]
    public class AudioData
    {
        [SerializeField] private string _id;
        [SerializeField] private AudioClip _clip;

        public string Id => _id;
        public AudioClip Clip => _clip;
    }
}
