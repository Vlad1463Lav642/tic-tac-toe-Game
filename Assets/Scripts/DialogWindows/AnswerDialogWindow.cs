﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerDialogWindow : BaseWindow
{
    public override void ConfirmButton()
    {
        Destroy(gameObject);
    }
}