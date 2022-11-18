using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    private Characters.Names followCharacter;

    private AudioListener audioListener;
    //private Camera camera;

    private void Awake() {
        followCharacter = (Characters.Names)System.Enum.Parse(typeof(Characters.Names),(transform.parent.name));
        audioListener = GetComponent<AudioListener>();
        //camera = GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CameraSwitch(bool onOff) {
        GetComponent<Camera>().enabled = onOff;
        audioListener.enabled = onOff;
    }

    public void ChangeActiveCamera(Characters.Names m_character) {
        CameraSwitch(followCharacter == m_character);
    }
}
