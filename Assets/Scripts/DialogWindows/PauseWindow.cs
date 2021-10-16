using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : BaseWindow
{

    [SerializeField] private GameObject optionsPanel;

    public override void ConfirmButton() {}

    public override void CloseButton()
    {
        gameObject.SetActive(false);
    }

    public void OptionsButton()
    {
        optionsPanel.SetActive(true);
    }
}