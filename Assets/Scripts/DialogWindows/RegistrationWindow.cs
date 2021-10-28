using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Окно регистрации.
/// </summary>
public class RegistrationWindow : BaseWindow
{
    #region Параметры
    private int i = 0;

    [Header("Поле ввода логина")]
    [SerializeField] private InputField userNameField;
    [Header("Окно авторизации")]
    [SerializeField] private LoginWindow loginPanel;
    [Header("Окно рейтинга игроков")]
    [SerializeField] private RatingPanelController ratingPanel;
    #endregion

    public override void ConfirmButton()
    {
        RegisterPlayer(userNameField.text);

        loginPanel.LoginDataLoad();
        ratingPanel.RatingDataLoad();
        userNameField.text = "";

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Осуществляет регистрацию игрока.
    /// </summary>
    /// <param name="playerName">Имя игрока.</param>
    private void RegisterPlayer(string playerName)
    {
        if (!PlayerPrefs.HasKey(playerName))
        {
            if (!PlayerPrefs.HasKey($"Player {i}"))
            {
                PlayerPrefs.SetString($"Player {i}", playerName);
                PlayerPrefs.SetInt(playerName, 0);
                PlayerPrefs.SetInt("PlayersCount", i + 1);
                PlayerPrefs.SetString("CurrentPlayerName", playerName);
            }
            else
            {
                i++;
                RegisterPlayer(playerName);
            }
        }
    }
}