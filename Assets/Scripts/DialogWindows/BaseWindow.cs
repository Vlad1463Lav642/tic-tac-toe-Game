using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseWindow : MonoBehaviour
{
    [SerializeField] private Text windowLabel;

    public virtual void CloseButton()
    {
        Destroy(gameObject);
    }

    public abstract void ConfirmButton();
}