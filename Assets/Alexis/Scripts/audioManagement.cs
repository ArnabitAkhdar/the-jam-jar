using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class audioManagement : MonoBehaviour
{
    private bool hasMusicSourceBeenSetup = false;

    public AudioSource musicSource, soundSource;

    void Start()
    {
        // Set volume to zero
        musicSource.volume = 0f;
    }

    void Update()
    {
        if (!hasMusicSourceBeenSetup)
        {
            musicSource.volume += Time.deltaTime / 4f;

            if(PlayerPrefs.GetFloat("musicSourceVolume") != 0)
            {
                if (musicSource.volume >= PlayerPrefs.GetFloat("musicSourceVolume"))
                {
                    hasMusicSourceBeenSetup = true;

                    musicSource.volume = PlayerPrefs.GetFloat("musicSourceVolume");
                }
            }
            else if(musicSource.volume >= 1f)
            {
                hasMusicSourceBeenSetup = true;

                musicSource.volume = 1f;
            }
        }
    }

    // Button Function
    public void playAudioClipButton(AudioClip _audioClip) 
    {
        soundSource.clip = _audioClip;
        soundSource.loop = false;
        soundSource.Play();
    }

    public void setMusicLevel(Slider _slider) 
    { 
        musicSource.volume = _slider.value;

        PlayerPrefs.SetFloat("musicSourceVolume", _slider.value);
    }
}
