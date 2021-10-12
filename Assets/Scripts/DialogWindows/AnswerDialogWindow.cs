using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerDialogWindow : BaseWindow
{
    public AnswerDialogWindow(int type) : base(type)
    {

    }

    public override void ConfirmButton()
    {
        Destroy(gameObject);
    }
}