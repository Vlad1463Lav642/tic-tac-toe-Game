using UnityEngine;

public class LoginWindow : BaseWindow
{
    [SerializeField] private GameObject registrationPanel;
    private string[] playerLogins;

    private void Start()
    {
        LoginDataLoad();
    }

    public void LoginDataLoad()
    {
        playerLogins = GetPlayerLogins();
        gameObject.GetComponent<ScrollViewAdapter>().AddItems(playerLogins, playerLogins);
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

    public string LoginPlayer()
    {
        return null;
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