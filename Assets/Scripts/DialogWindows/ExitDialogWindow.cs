using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDialogWindow : BaseWindow
{
    public ExitDialogWindow(int type) : base(type)
    {

    }

    public override void ConfirmButton()
    {
        Application.Quit();
        Destroy(gameObject);
    }
}