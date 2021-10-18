using UnityEngine;

/// <summary>
/// Диалоговое окно потверждения выхода.
/// </summary>
public class ExitDialogWindow : BaseWindow
{
    public override void ConfirmButton()
    {
        gameObject.SetActive(false);
        Application.Quit();
    }
}