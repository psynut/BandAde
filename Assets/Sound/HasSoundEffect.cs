using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HasSoundEffect : MonoBehaviour
{
    public AudioClip[] soundEffects;

    private AudioSource audioSource;
    private SoundEffectCatcher soundEffectCatcher;


    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    void Start() {
        soundEffectCatcher = GameObject.FindWithTag("Audio").GetComponent<SoundEffectCatcher>();

        if(audioSource) {
            soundEffectCatcher.AddSEAudioSource(audioSource);
            audioSource.loop = false;
            audioSource.playOnAwake = false;
        } else {
            Debug.LogWarning(this.name + "has HasSoundEffect component attached, but no AudioSource was found to send to SoundEffectCatcher");
        }
    }

    public int SoundEffectsCount() {
        return soundEffects.Length;
    }

    public void PlayRandomSoundEffect() {
        if(soundEffects.Length > 0) {

            audioSource.clip = soundEffects[Random.Range(0,soundEffects.Length)];
            audioSource.Play();
        } else {
            Debug.LogWarning(this.name + " HasSoundEffect.PlayRandomSoundEffect called, but there is no sound effects added to the list");
        }
    }

    public void PlaySoundEffect(int clipNumber) {
        audioSource.clip = soundEffects[clipNumber];
        audioSource.Play();
    }



}
