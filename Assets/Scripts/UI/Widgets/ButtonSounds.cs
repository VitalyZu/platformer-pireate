using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] AudioClip _clip;
    private AudioSource _source;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(_source == null)
        _source = GameObject.FindWithTag("SFXAudioSource").GetComponent<AudioSource>();
        
        _source.PlayOneShot(_clip);
    }
}
