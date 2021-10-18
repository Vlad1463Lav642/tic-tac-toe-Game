using UnityEngine;

/// <summary>
/// Скрипт осуществляющий управление звуками кнопок.
/// </summary>
public class SoundForButtons : MonoBehaviour
{
    #region Параметры
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioHover;
    [SerializeField] private AudioClip audioPress;
    #endregion

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void HoverSound()
    {
        audioSource.PlayOneShot(audioHover);
    }

    public void PressSound()
    {
        audioSource.PlayOneShot(audioPress);
    }
}