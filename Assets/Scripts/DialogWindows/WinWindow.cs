using UnityEngine;
using UnityEngine.UI;

public class WinWindow : BaseWindow
{
    [SerializeField] private Text scoreLabel;

    public override void ConfirmButton() {}

    public void SetLabelScore(string score)
    {
        scoreLabel.text = score;
    }
}