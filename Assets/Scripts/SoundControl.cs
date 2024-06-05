using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    private AudioSource _audio;


    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void ToggleSound()
    {
        _audio.mute = !_audio.mute;
    }
}
