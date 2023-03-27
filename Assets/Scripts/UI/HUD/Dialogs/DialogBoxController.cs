using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogBoxController : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] GameObject _container;
    [SerializeField] Animator _animator;
    
    [Space]
    [SerializeField] float _textSpeed = 0.09f;

    [Header("Sounds")]
    [SerializeField] AudioClip _typingClip;
    [SerializeField] AudioClip _openClip;
    [SerializeField] AudioClip _closeClip;

    private DialogData _data;
    private int _currentSentence;
    private AudioSource _audioSource;
    private Coroutine _typingRoutine;
    private UnityEvent _onComplete;

    private readonly int _isOpenKey = Animator.StringToHash("isOpen");

    private void Start()
    {
        _audioSource = AudioUtils.FindSfxSource();
    }
    public void ShowDialog(DialogData data, UnityEvent call)
    {
        _onComplete = call;
        _data = data;
        _currentSentence = 0;
        _text.text = string.Empty;

        _container.SetActive(true);
        _audioSource.PlayOneShot(_openClip);
        _animator.SetBool(_isOpenKey, true);
    }

    private IEnumerator TypeDialogTetx()
    {
        _text.text = string.Empty;
        var sentence = _data.Sentences[_currentSentence];
        foreach (var letter in sentence)
        {
            _text.text += letter;
            _audioSource.PlayOneShot(_typingClip);
            yield return new WaitForSeconds(_textSpeed);
        }
        _typingRoutine = null;
    }

    public void OnSkip()
    {
        if (_typingRoutine == null) return;

        StopTypeAnimation();

    }

    public void OnContinue()
    {
        StopTypeAnimation();
        _currentSentence++;

        var isDialogComplete = _currentSentence >= _data.Sentences.Length;
        
        if (isDialogComplete)
        {
            HideDialogBox();
        }
        else
        {
            OnStartDialogAnimation();
        }
    }

    private void HideDialogBox()
    {
        _onComplete?.Invoke();
        _animator.SetBool(_isOpenKey, false);
        _audioSource.PlayOneShot(_closeClip);
    }

    private void StopTypeAnimation()
    {
        if (_typingRoutine != null) StopCoroutine(_typingRoutine);
        _typingRoutine = null;
        _text.text = _data.Sentences[_currentSentence];

    }

    private void OnStartDialogAnimation()
    {
        _typingRoutine = StartCoroutine(TypeDialogTetx());
    }

    private void OnClsoeDialogAnimationComplete()
    {

    }

    [SerializeField] private DialogData _dataTest;
    public void Test()
    {
        //ShowDialog(_dataTest);
    }
}
