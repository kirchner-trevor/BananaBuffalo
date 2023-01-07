using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PitchRandomizer : MonoBehaviour
{
    private AudioSource AudioSource;

    [Range(-3, 3)] public float MinPitch = 1;
    [Range(-3, 3)] public float MaxPitch = 1;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AudioSource.pitch = Random.Range(MinPitch, MaxPitch);
    }
}
