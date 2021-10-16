using UnityEngine;

public class ExitDialogWindow : BaseWindow
{
    public override void ConfirmButton()
    {
        gameObject.SetActive(false);
        Application.Quit();
    }
}