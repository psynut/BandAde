using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectCatcher : MonoBehaviour
{
    //HasSoundEffect finds this script through the "Audio" Tag on the Sound Manager.
    //HasSoundEffect adds the AudioSource component to the list in order to track all of the sound effects
    //This allows for the ability to also change the level of the effect as needed.

    private List<AudioSource> sEAudioSources;
    private SoundManager soundManager;

    private void Awake() {
        sEAudioSources = new List<AudioSource>();
        soundManager = GetComponent<SoundManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSEAudioSource(AudioSource newAudioSource) {
        sEAudioSources.Add(newAudioSource);
        newAudioSource.volume = ((float)soundManager.MusicVolume / 100f);
    }

    public void AdjustSEVolume(int newVolume) {
        foreach(AudioSource audioSource in sEAudioSources) {
            audioSource.volume = ((float)newVolume) / 100f;
        }
    }
}
