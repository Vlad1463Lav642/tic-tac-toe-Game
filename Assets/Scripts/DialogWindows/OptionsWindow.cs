using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Окно настроек.
/// </summary>
public class OptionsWindow : BaseWindow
{
    #region Параметры
    [SerializeField] private new AudioMixer audio;
    [SerializeField] private Slider audioSlider;
    #endregion

    private void Start()
    {
        if (PlayerPrefs.HasKey("SoundVolume"))
            audioSlider.value = PlayerPrefs.GetFloat("SoundVolume");
    }

    /// <summary>
    /// Устанавливает значение громкости.
    /// </summary>
    /// <param name="volume">Значение громкости.</param>
    public void SetVolume(float volume)
    {
        audio.SetFloat("SoundVolume", volume);
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    public override void ConfirmButton()
    {
        gameObject.SetActive(false);
    }
}