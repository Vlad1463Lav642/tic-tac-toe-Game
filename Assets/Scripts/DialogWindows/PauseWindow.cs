using UnityEngine;

/// <summary>
/// Окно паузы.
/// </summary>
public class PauseWindow : BaseWindow
{
    #region Параметры
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject confirmPanel;
    #endregion

    public override void ConfirmButton()
    {
        confirmPanel.SetActive(true);
    }

    public override void CloseButton()
    {
        gameObject.SetActive(false);
    }

    public void OptionsButton()
    {
        optionsPanel.SetActive(true);
    }
}