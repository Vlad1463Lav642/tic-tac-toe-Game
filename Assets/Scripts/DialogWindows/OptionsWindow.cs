using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsWindow : BaseWindow
{
    public OptionsWindow(int type) : base(type)
    {

    }

    public override void ConfirmButton()
    {
        Destroy(gameObject);
    }
}