using UnityEngine;

public class ExitDialogWindow : BaseWindow
{
    public override void ConfirmButton()
    {
        Destroy(gameObject, 0.2f);
        Application.Quit();
    }
}