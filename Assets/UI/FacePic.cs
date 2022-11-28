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
    [HideInInspector]
    public Image image;
    private float startTime;

    private void Awake() {
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CharacterSpeaks(int character, float seconds) {
        startTime = Time.time;
        StartCoroutine(AnimatePictures(character,seconds,0));
    }

    IEnumerator AnimatePictures(int character, float seconds, int sequence) {
        image.sprite = characterImages[character].sprites[sequence];
        sequence++;
        yield return new WaitForSeconds(.1f);
        if(Time.time > startTime + seconds) {
            image.sprite = characterImages[character].sprites[0];
        } else {
            if(sequence == characterImages[character].sprites.Length) {
                sequence = 0;
            }
            StartCoroutine(AnimatePictures(character,seconds,sequence));
        }
    }
}
