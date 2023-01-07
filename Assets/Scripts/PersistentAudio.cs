using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentAudio : MonoBehaviour
{
	public AudioSource[] audioSourceArray;
    private int sourceToggle;
    private double nextStartTime = 0;
    private AudioClip clipToPlay;
    public AudioClip titleSong;
    public AudioClip gameSong;

    void Update()
    {
        if (AudioSettings.dspTime > nextStartTime - 1)
        {
            // Loads the next Clip to play and schedules when it will start
            audioSourceArray[sourceToggle].clip = clipToPlay;
            audioSourceArray[sourceToggle].PlayScheduled(nextStartTime);
            // Checks how long the Clip will last and updates the Next Start Time with a new value
            //double duration = clipToPlay.length;
            double duration = (double)clipToPlay.samples / clipToPlay.frequency;
            nextStartTime = nextStartTime + duration;
            // Switches the toggle to use the other Audio Source next
            sourceToggle = 1 - sourceToggle;
        }
    }

    public static PersistentAudio Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += ChangeSong;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
   private void ChangeSong(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex == 0)
        {
            clipToPlay = titleSong;
        }
        else if (arg0.buildIndex == 3)
        {
            clipToPlay = gameSong;
        }
    }
 }
