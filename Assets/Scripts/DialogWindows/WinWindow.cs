using UnityEngine;

/// <summary>
/// Окно победы.
/// </summary>
public class WinWindow : BaseWindow
{
    [SerializeField] private GameObject confirmPanel;

    public override void ConfirmButton()
    {
        confirmPanel.SetActive(true);
    }
}