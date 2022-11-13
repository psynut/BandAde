using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacePic : MonoBehaviour
{
    [System.Serializable]
    public class CharacterSprites{
        public Sprite[] sprites;
    }

    public CharacterSprites[] characterImages;

    private Image image;
    private float startTime;

    private void Awake() {
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CharacterSpeaks(0,20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CharacterSpeaks(int character, float seconds) {
        startTime = Time.time;
        StartCoroutine(SwitchOutFace(character,seconds,0));
    }

    IEnumerator SwitchOutFace(int character, float seconds, int sequence) {
        Debug.Log(sequence);
        Debug.Log("running SwitchOutFace");
        image.sprite = characterImages[character].sprites[sequence];
        sequence++;
        yield return new WaitForSeconds(.1f);
        if(Time.time > startTime + seconds) {
            image.sprite = characterImages[character].sprites[0];
        } else {
            if(sequence == characterImages[character].sprites.Length) {
                sequence = 0;
            }
            StartCoroutine(SwitchOutFace(character,seconds,sequence));
        }
    }
}
