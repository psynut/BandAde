using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnToMenuButton : MonoBehaviour
{

    Button button;
    SceneLoader sceneLoader;

    private void Awake() {
        button = GetComponent<Button>();
        button.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        if(sceneLoader != null) {
            button.onClick.AddListener(delegate{sceneLoader.LoadScene(1);});
        }
        Invoke("EnableButton",10f); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnableButton() {
        button.enabled = true;
    }

    public void Testbutton() {
        Debug.Log("Test Button");
    }
    
}
