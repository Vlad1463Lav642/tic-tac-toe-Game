using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : BaseWindow
{

    [SerializeField] private GameObject optionsPanel;

    public override void ConfirmButton() {}

    public override void CloseButton()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void OptionsButton()
    {
        Time.timeScale = 1f;
        optionsPanel.SetActive(true);
    }
}