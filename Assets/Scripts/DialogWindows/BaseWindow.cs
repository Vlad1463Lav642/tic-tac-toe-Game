﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseDialogWindow : MonoBehaviour
{
    [SerializeField] private Text windowLabel;

    private int windowType;

    public int WindowType
    {
        get
        {
            return windowType;
        }
    }

    public BaseDialogWindow(int type)
    {
        windowType = type;
    }

    public virtual void CloseButton()
    {
        Destroy(gameObject);
    }

    public abstract void ConfirmButton();
}