using UnityEngine;
using UnityEngine.UI;

public abstract class BaseWindow : MonoBehaviour
{
    [SerializeField] private Text windowLabel;

    public virtual void CloseButton()
    {
        Destroy(gameObject,0.2f);
    }

    public abstract void ConfirmButton();
}