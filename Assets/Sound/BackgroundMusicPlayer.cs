using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicPlayer : MonoBehaviour
{

    public AudioClip[] tracks;

    public enum PlayCommand {
        Once,
        Loop
    }

    [System.Serializable]
    public class SceneCommand {
        public PlayCommand playCommand;
        public int trackForScene;
    }

    public SceneCommand[] sceneCommands;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        PlayTrack(sceneCommands[scene.buildIndex].trackForScene, (sceneCommands[scene.buildIndex].playCommand == PlayCommand.Loop));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustMusicVolume(int newValue) {
        audioSource.volume = ((float)newValue) / 100f;
    }

    private void PlayTrack(int trackNumber, bool playLoop) {
        audioSource.clip = tracks[trackNumber];
        audioSource.loop = playLoop;
        audioSource.Play();
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
