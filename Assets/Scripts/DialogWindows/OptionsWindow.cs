using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsWindow : BaseWindow
{
    [SerializeField] private new AudioMixer audio;
    [SerializeField] private Slider audioSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            audioSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        }
    }

    public void SetVolume(float volume)
    {
        audio.SetFloat("SoundVolume", volume);
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    public override void ConfirmButton()
    {
        //Destroy(gameObject, 0.2f);
        gameObject.SetActive(false);
    }
}