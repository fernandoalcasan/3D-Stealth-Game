using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioClip _voiceOverAudio;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            AudioManager.Instance.PlayVoiceOverAudio(_voiceOverAudio);
    }
}
