using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuController : MonoBehaviour
{
    #region Параметры
    [Header("Список с диалоговыми окнами")]
    [SerializeField] private List<GameObject> windowPanels;
    [SerializeField] private new AudioMixer audio;

    [Header("Окно авторизации")]
    [SerializeField] private GameObject loginPanel;
    [Header("Окно выхода из игры")]
    [SerializeField] private GameObject exitPanel;
    [Header("Окно настроек")]
    [SerializeField] private GameObject optionsPanel;
    #endregion

    private void Start()
    {
        audio.SetFloat("SoundVolume", PlayerPrefs.GetFloat("SoundVolume"));

        if (!PlayerPrefs.HasKey("CurrentPlayerName"))
            PlayerPrefs.SetString("CurrentPlayerName", "Guest");
    }

    public void Authorization() => loginPanel.SetActive(true);


    public void Settings() => optionsPanel.SetActive(true);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            exitPanel.SetActive(true);
    }
}