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

    public void ChangeVolumeByMultiplier(float multiplier)
    {
        GameObject eternalRadio = GameObject.Find("EternalRadio");
        if (eternalRadio != null)
        {
            AudioSource[] audioSources = eternalRadio.GetComponentsInChildren<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.volume *= multiplier;
            }
        }
        else
        {
            Debug.LogError($"Cannot find 'EternalRadio' to change volume.");
        }
    }
}
