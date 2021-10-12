using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmDialogWindow : BaseWindow
{
    public ConfirmDialogWindow(int type) : base(type)
    {

    }

    public override void ConfirmButton()
    {
        Destroy(gameObject);
    }
}