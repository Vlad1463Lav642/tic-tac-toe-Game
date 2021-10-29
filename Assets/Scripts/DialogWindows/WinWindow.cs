using UnityEngine;

/// <summary>
/// Окно победы.
/// </summary>
public class WinWindow : BaseWindow
{
    #region Параметры
    [Header("Окно подтверждения")]
    [SerializeField] private GameObject confirmPanel;
    #endregion

    public override void ConfirmButton()
    {
        confirmPanel.SetActive(true);
    }
}