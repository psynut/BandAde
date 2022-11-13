using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private int musicVolume;

    public int MusicVolume {
        get {return musicVolume;}
        set {
            musicVolume = value;
            PlayerPrefs.SetInt(MUSIC_VOLUME_KEY,value);
            backgroundMusicPlayer.AdjustMusicVolume(value);
        }
    }

    private int sEVolume;

    public int SEVolume {
        get {return sEVolume;}
        set {
            sEVolume = value;
            PlayerPrefs.SetInt(SE_VOLUME_KEY,value);
            soundEffectCatcher.AdjustSEVolume(value);
        }
    }

    const string MUSIC_VOLUME_KEY = "music_volume";
    const string SE_VOLUME_KEY = "se_volume";

    private SoundEffectCatcher soundEffectCatcher;
    private BackgroundMusicPlayer backgroundMusicPlayer;

    private void Awake() {
        soundEffectCatcher = GetComponent<SoundEffectCatcher>();
        backgroundMusicPlayer = GetComponent<BackgroundMusicPlayer>();
    }

    // Start is called before the first frame update
    void Start() {
        if(PlayerPrefs.GetInt(MUSIC_VOLUME_KEY) > 100 || PlayerPrefs.GetInt(MUSIC_VOLUME_KEY) <= 0) {
            MusicVolume = 50;
        } else {
            MusicVolume = PlayerPrefs.GetInt(MUSIC_VOLUME_KEY);
        }
        if(PlayerPrefs.GetInt(SE_VOLUME_KEY) > 100 || PlayerPrefs.GetInt(SE_VOLUME_KEY) <= 0) {
            SEVolume = 50;
        } else {
            SEVolume = PlayerPrefs.GetInt(SE_VOLUME_KEY);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
