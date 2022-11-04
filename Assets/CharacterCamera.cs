using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    public CharController.CharacterNames followCharacter;

    private AudioListener audioListener;
    private Camera camera;

    private void Awake() {
        followCharacter = (CharController.CharacterNames)System.Enum.Parse(typeof(CharController.CharacterNames),(transform.parent.name));
        audioListener = GetComponent<AudioListener>();
        camera = GetComponent<Camera>();
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
        camera.enabled = onOff;
        audioListener.enabled = onOff;
    }

    public void ChangeActiveCamera(CharController.CharacterNames m_character) {
        CameraSwitch(followCharacter == m_character);
    }
}
