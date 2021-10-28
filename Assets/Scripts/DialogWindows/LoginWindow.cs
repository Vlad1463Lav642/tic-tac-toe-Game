using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// Окно авторизации.
/// </summary>
public class LoginWindow : BaseWindow
{
    #region Параметры
    [SerializeField] private GameObject registrationPanel;
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
        var playerLogins = GetPlayerLogins();
        gameObject.GetComponent<ScrollViewAdapter>().AddItems(playerLogins);
        onPlayerSelected = LoginPlayer;
        ButtonsInit();
    }

    /// <summary>
    /// Возвращает массив логинов игроков.
    /// </summary>
    /// <returns></returns>
    public List<string> GetPlayerLogins()
    {
        var playerLoginsCount = PlayerPrefs.GetInt("PlayersCount");
        var playerLogins = new List<string>();

        for(int i = 0;i < playerLoginsCount; i++)
        {
            playerLogins.Add(PlayerPrefs.GetString($"Player {i}"));
        }

        return playerLogins;
    }

    /// <summary>
    /// Инициализация кнопок игроков в списке.
    /// </summary>
    private void ButtonsInit()
    {
        var itemsList = gameObject.GetComponent<ScrollViewAdapter>().GetItems();

        itemsList.Select(item => { item.GetComponent<Button>().onClick.AddListener(onPlayerSelected); return item; }).Count();

    }

    /// <summary>
    /// Возвращает массив ключей по которым можно получить логин из PlayerPrefs.
    /// </summary>
    /// <returns></returns>
    public List<string> GetPlayerKeys()
    {
        var playerKeysCount = PlayerPrefs.GetInt("PlayersCount");
        List<string> playerKeys = new List<string>();

        for(int i = 0; i < playerKeysCount; i++)
        {
            playerKeys.Add($"Player {i}");
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