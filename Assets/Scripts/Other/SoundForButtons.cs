using UnityEngine;

public class SoundForButtons : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioHover;
    [SerializeField] private AudioClip audioPress;

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