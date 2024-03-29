using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Audio manager is NULL");

            return _instance;
        }
    }

    [SerializeField]
    private AudioSource _voiceOverAS;
    [SerializeField]
    private AudioSource _sfxAS;

    private void Awake()
    {
        _instance = this;
    }

    public void PlayVoiceOverAudio(AudioClip clip)
    {
        _voiceOverAS.clip = clip;
        _voiceOverAS.Play();
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        _sfxAS.PlayOneShot(clip, volume);
    }
}
