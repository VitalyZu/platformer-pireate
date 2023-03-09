using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxController : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] GameObject _container;
    [SerializeField] Animator _animator;
    
    [Space]
    [SerializeField] float _textSpeed = 0.09f;

    [Header("Sounds")]
    [SerializeField] AudioClip _typing;
    [SerializeField] AudioClip _open;
    [SerializeField] AudioClip close;

    private DialogData _data;
    private int _currentSentence;
    private AudioSource _audioSource;

    private readonly int _isOpenKey = Animator.StringToHash("isOpen");

    private void Start()
    {
        _audioSource = AudioUtils.FindSfxSource();
    }
    public void ShowDialog(DialogData data)
    {
        _data = data;
        _currentSentence = 0;
        _text.text = string.Empty;

        _container.SetActive(true);
        _audioSource.PlayOneShot(_open);
        _animator.SetBool(_isOpenKey, true);
    }

    [SerializeField] private DialogData _dataTest;
    public void Test()
    {
        ShowDialog(_dataTest);
    }
}
