using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDialogWindow : BaseWindow
{
    public override void ConfirmButton()
    {
        Application.Quit();
        Destroy(gameObject);
    }
}