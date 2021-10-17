using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoginWindow : BaseWindow
{
    [SerializeField] private GameObject registrationPanel;
    private string[] playerLogins;
    private UnityAction onPlayerSelected;

    private void Start()
    {
        LoginDataLoad();
    }

    public void LoginDataLoad()
    {
        playerLogins = GetPlayerLogins();
        gameObject.GetComponent<ScrollViewAdapter>().AddItems(playerLogins);
        onPlayerSelected = LoginPlayer;
        ButtonsInit(playerLogins);
    }

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

    private void ButtonsInit(string[] userNames)
    {
        List<GameObject> itemsList = gameObject.GetComponent<ScrollViewAdapter>().GetItems();

        foreach(var item in itemsList)
        {
            item.GetComponent<Button>().onClick.AddListener(onPlayerSelected);
        }

    }

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