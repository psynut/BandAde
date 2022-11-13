using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public enum Command { LoadNextOnDelay, LoadNextOnCall };
    [System.Serializable]
    public class SceneCommand {
        public Command command;
        public float delayTime;
    }

    public SceneCommand[] sceneCommands;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if(sceneCommands[scene.buildIndex].command == Command.LoadNextOnDelay) {
            Invoke("LoadNextScene",sceneCommands[scene.buildIndex].delayTime);
        }
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
