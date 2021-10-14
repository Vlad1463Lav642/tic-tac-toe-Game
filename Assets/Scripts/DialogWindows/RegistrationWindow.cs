using UnityEngine;
using UnityEngine.UI;

public class RegistrationWindow : BaseWindow
{
    private int i = 0;
    [SerializeField] private InputField userNameField;
    [SerializeField] private LoginWindow loginPanel;
    [SerializeField] private RatingPanelController ratingPanel;

    public override void ConfirmButton()
    {
        RegisterPlayer(userNameField.text);

        loginPanel.LoginDataLoad();
        ratingPanel.RatingDataLoad();

        gameObject.SetActive(false);
    }

    private void RegisterPlayer(string playerName)
    {
        if (!PlayerPrefs.HasKey(playerName))
        {
            if (!PlayerPrefs.HasKey($"Player {i}"))
            {
                PlayerPrefs.SetString($"Player {i}", playerName);
                PlayerPrefs.SetInt(playerName, 0);
                PlayerPrefs.SetInt("PlayersCount", i + 1);
            }
            else
            {
                i++;
                RegisterPlayer(playerName);
            }
        }
    }
}