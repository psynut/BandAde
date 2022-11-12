using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicPlayer : MonoBehaviour
{

    public AudioClip[] tracks;
    public int[] trackToPlayonScene;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        PlayTrack(trackToPlayonScene[scene.buildIndex]);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayTrack(int trackNumber) {
        audioSource.clip = tracks[trackNumber];
        audioSource.loop = true;
        audioSource.Play();
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


}
