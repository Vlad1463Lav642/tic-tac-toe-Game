using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Окно авторизации.
/// </summary>
public class LoginWindow : BaseWindow
{
    #region Параметры
    [SerializeField] private GameObject registrationPanel;
    private string[] playerLogins;
    private UnityAction onPlayerSelected;
    #endregion

    private void Start()
    {
        LoginDataLoad();
    }

    /// <summary>
    /// Передает данные в ScrollViewAdapter для вывода в списке.
    /// </summary>
    public void LoginDataLoad()
    {
        playerLogins = GetPlayerLogins();
        gameObject.GetComponent<ScrollViewAdapter>().AddItems(playerLogins);
        onPlayerSelected = LoginPlayer;
        ButtonsInit();
    }

    /// <summary>
    /// Возвращает массив логинов игроков.
    /// </summary>
    /// <returns></returns>
    public string[] GetPlayerLogins()
    {
        int playerLoginsCount = PlayerPrefs.GetInt("PlayersCount");
        string[] playerLogins = new string[playerLoginsCount];

        for(int i = 0;i < playerLoginsCount; i++)
        {
            playerLogins[i] = PlayerPrefs.GetString($"Player {i}");
        }

        return playerLogins;
    }

    /// <summary>
    /// Инициализация кнопок игроков в списке.
    /// </summary>
    private void ButtonsInit()
    {
        List<GameObject> itemsList = gameObject.GetComponent<ScrollViewAdapter>().GetItems();

        foreach(var item in itemsList)
        {
            item.GetComponent<Button>().onClick.AddListener(onPlayerSelected);
        }

    }

    /// <summary>
    /// Возвращает массив ключей по которым можно получить логин из PlayerPrefs.
    /// </summary>
    /// <returns></returns>
    public string[] GetPlayerKeys()
    {
        int playerKeysCount = PlayerPrefs.GetInt("PlayersCount");
        string[] playerKeys = new string[playerKeysCount];

        for(int i = 0; i < playerKeysCount; i++)
        {
            playerKeys[i] = $"Player {i}";
        }

        return playerKeys;
    }

    /// <summary>
    /// Осуществляет перезапись значения текущего имени игрока в PlayerPrefs при нажатии на кнопку любого игрока в окне авторизации. 
    /// </summary>
    private void LoginPlayer()
    {
        PlayerPrefs.SetString("CurrentPlayerName", EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text);
    }

    public override void ConfirmButton()
    {
        gameObject.SetActive(false);
    }

    public override void CloseButton()
    {
        gameObject.SetActive(false);
    }

    public void RegistrationButton()
    {
        registrationPanel.SetActive(true);
    }
}