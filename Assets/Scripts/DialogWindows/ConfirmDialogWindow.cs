using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmDialogWindow : BaseWindow
{
    public override void ConfirmButton()
    {
        Destroy(gameObject);
    }
}