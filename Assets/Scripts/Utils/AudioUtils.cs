using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUtils : MonoBehaviour
{
    public const string SfxSourceTag = "SFXAudioSource";

    public static AudioSource FindSfxSource()
    {
        return GameObject.FindWithTag(SfxSourceTag).GetComponent<AudioSource>();
    }
}
