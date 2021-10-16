using UnityEngine;
using UnityEngine.UI;

public abstract class BaseWindow : MonoBehaviour
{
    [SerializeField] private Text windowLabel;

    public virtual void CloseButton()
    {
        gameObject.SetActive(false);
    }

    public virtual void SetLabelText(string text)
    {
        windowLabel.text = text;
    }

    public abstract void ConfirmButton();
}