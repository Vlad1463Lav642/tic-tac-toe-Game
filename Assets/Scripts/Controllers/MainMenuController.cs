using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuController : MonoBehaviour
{
    [Header("Список с диалоговыми окнами")]
    [SerializeField] private List<GameObject> windowPanels;
    [SerializeField] private new AudioMixer audio;

    private void Start()
    {
        audio.SetFloat("SoundVolume", PlayerPrefs.GetFloat("SoundVolume"));
        Debug.Log(PlayerPrefs.GetFloat("SoundVolume"));
    }


    public void Settings()
    {
        Instantiate(windowPanels[1], transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Instantiate(windowPanels[0],transform);
        }
    }
}