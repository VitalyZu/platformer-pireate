using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarWidget : MonoBehaviour
{
    [SerializeField] Image _image;

    public void SetProgress(float progress)
    {
        _image.fillAmount = progress;
    }
}
