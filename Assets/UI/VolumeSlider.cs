using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VolumeSlider : MonoBehaviour
{
    public enum Type {SoundEffect, Music};
    public Type myType;

    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    public void SliderMoved(float value) {
        if(myType == Type.Music) {
            soundManager.MusicVolume = (int)value;
        }
        if(myType == Type.SoundEffect) {
            soundManager.SEVolume = (int)value;
        }
    }
}
