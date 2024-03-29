﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowUtils
{
    public static void CreateWindow(string resourcePath)
    {
        var window = Resources.Load<GameObject>(resourcePath);
        var canvas = GameObject.FindGameObjectWithTag("MainUICanvas").GetComponent<Canvas>();
        Object.Instantiate(window, canvas.transform);
    }
}
