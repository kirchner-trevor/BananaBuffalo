using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefPersistentAudio : MonoBehaviour
{
    public void PlayOneShot(AudioClip audioClip)
    {
        GameObject soundEffectSource = GameObject.Find("SoundEffectSource");
        if (soundEffectSource != null)
        {
            AudioSource audioSource = soundEffectSource.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.LogError($"Cannot find 'SoundEffectSource' to play clip {audioClip.name}.");
        }
    }
}
