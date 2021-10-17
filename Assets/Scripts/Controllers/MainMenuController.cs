using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuController : MonoBehaviour
{
    [Header("Список с диалоговыми окнами")]
    [SerializeField] private List<GameObject> windowPanels;
    [SerializeField] private new AudioMixer audio;
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject optionsPanel;

    private void Start()
    {
        audio.SetFloat("SoundVolume", PlayerPrefs.GetFloat("SoundVolume"));

        if (!PlayerPrefs.HasKey("CurrentPlayerName"))
        {
            PlayerPrefs.SetString("CurrentPlayerName", "Guest");
        }
    }

    public void Authorization()
    {
        loginPanel.SetActive(true);
    }


    public void Settings()
    {
        optionsPanel.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPanel.SetActive(true);
        }
    }
}