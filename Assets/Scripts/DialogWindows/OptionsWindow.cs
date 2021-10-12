using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsWindow : BaseWindow
{
    [SerializeField] private new AudioMixer audio;

    public void SetVolume(float volume)
    {
        audio.SetFloat("Volume", volume);
    }

    public override void ConfirmButton()
    {
        Destroy(gameObject);
    }
}