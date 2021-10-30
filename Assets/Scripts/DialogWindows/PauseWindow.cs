using UnityEngine;

/// <summary>
/// Окно паузы.
/// </summary>
public class PauseWindow : BaseWindow
{
    #region Параметры
    [Header("Окно настроек")]
    [SerializeField] private GameObject optionsPanel;
    [Header("Окно подтверждения")]
    [SerializeField] private GameObject confirmPanel;
    #endregion

    public override void ConfirmButton() => confirmPanel.SetActive(true);

    public override void CloseButton() => gameObject.SetActive(false);

    public void OptionsButton() => optionsPanel.SetActive(true);
}